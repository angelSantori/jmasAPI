using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jmasAPI;
using jmasAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text.Json;

namespace jmasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;        
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IHttpClientFactory _httpClientFactory;

        public UsersController(ApplicationDbContext context, IPasswordHasher<Users> passwordHasher, IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _httpClientFactory = clientFactory;
        }        

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.Include(u => u.role).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users
                .Include(u => u.role)
                .FirstOrDefaultAsync(u => u.Id_User == id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // GET: api/Padrons/BuscarPorNombre?nombre={nombre}
        [HttpGet("UserPorNombre")]
        public async Task<ActionResult<IEnumerable<Users>>> UserPorNombre([FromQuery] string userNombre)
        {
            if (string.IsNullOrWhiteSpace(userNombre))
            {
                return BadRequest("El parámetro 'userNombre' es requerido");
            }

            return await _context.Users
                .Where(uN => uN.User_Name != null &&
                             uN.User_Name.ToLower().Contains(userNombre.ToLower()))
                .Take(30)
                .ToListAsync();
        }

        // PUT: api/Users/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.Id_User)
            {
                return BadRequest("ID del usuario no coincide");
            }

            // Obtener el usuario existente con su rol
            var existingUser = await _context.Users
                .Include(u => u.role)
                .FirstOrDefaultAsync(u => u.Id_User == id);

            if (existingUser == null)
            {
                return NotFound("Usuario no encontrado");
            }

            // Validar unicidad de User_Contacto y User_Access (excepto para el mismo usuario)
            if (await _context.Users.AnyAsync(u => u.User_Contacto == users.User_Contacto && u.Id_User != id))
            {
                return Conflict("El contacto ya está en uso por otro usuario");
            }

            if (await _context.Users.AnyAsync(u => u.User_Access == users.User_Access && u.Id_User != id))
            {
                return Conflict("La palabra de acceso ya está en uso por otro usuario");
            }

            // Manejar el cambio de rol si es necesario
            if (users.idRole.HasValue)
            {
                var newRole = await _context.Role.FindAsync(users.idRole);
                if (newRole == null)
                {
                    return BadRequest("El rol especificado no existe");
                }
                existingUser.role = newRole;
                existingUser.idRole = users.idRole;
            }
            else
            {
                // Si no se envía idRole, mantener el actual o establecerlo como null
                existingUser.idRole = existingUser.idRole;
            }

            // Actualizar los campos básicos
            existingUser.User_Name = users.User_Name;
            existingUser.User_Contacto = users.User_Contacto;
            existingUser.User_Access = users.User_Access;
            existingUser.User_Rol = users.User_Rol;

            // Actualizar campos biométricos solo si se proporcionan nuevos valores
            if (!string.IsNullOrEmpty(users.User_Rostro64))
            {
                existingUser.User_Rostro64 = users.User_Rostro64;
            }

            if (!string.IsNullOrEmpty(users.User_HuellaFacial))
            {
                existingUser.User_HuellaFacial = users.User_HuellaFacial;
            }

            // Manejar cambio de contraseña (solo si se proporcionó una nueva)
            if (!string.IsNullOrEmpty(users.User_Password) && users.User_Password != existingUser.User_Password)
            {
                existingUser.User_Password = _passwordHasher.HashPassword(existingUser, users.User_Password);
            }

            try
            {
                await _context.SaveChangesAsync();

                // Cargar el rol actualizado para la respuesta
                await _context.Entry(existingUser)
                    .Reference(u => u.role)
                    .LoadAsync();

                await ReplicaUserNube(users, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(existingUser);
        }

        // POST: api/Users        
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            // 1. Validar unicidad de campos
            if (await _context.Users.AnyAsync(u => u.User_Contacto == users.User_Contacto))
                return Conflict("El contacto ya está en uso");

            if (await _context.Users.AnyAsync(u => u.User_Access == users.User_Access))
                return Conflict("La palabra de acceso ya está en uso");

            // 2. Validar que el rol exista (si se especificó)
            if (users.idRole.HasValue)
            {
                var roleExists = await _context.Role.AnyAsync(r => r.idRole == users.idRole);
                if (!roleExists)
                    return BadRequest("El rol especificado no existe");

                // Cargar el rol primero para asegurar la relación
                var role = await _context.Role.FindAsync(users.idRole);
                users.role = role; // Establecer la referencia completa
            }
            else
            {
                users.role = null; // Asegurarse que sea null si no hay idRole
            }

            // 3. Hash de la contraseña
            users.User_Password = _passwordHasher.HashPassword(users, users.User_Password);

            // 4. Guardar el usuario
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            // 5. Forzar la carga del rol para la respuesta
            await _context.Entry(users)
                .Reference(u => u.role)
                .LoadAsync();

            await ReplicaUserNube(users, "POST");

            return CreatedAtAction("GetUsers", new { id = users.Id_User }, users);
        }

        //Inicio de sesión Login        
        public class LoginRequest
        {
            public string UserAccess { get; set; }
            public string UserPassword { get; set; }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _context.Users
                .Include(u => u.role)
                .FirstOrDefaultAsync(u => u.User_Access == loginRequest.UserAccess);

            if (user == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            // Validar contraseña
            var result = _passwordHasher.VerifyHashedPassword(user, user.User_Password, loginRequest.UserPassword);
            if (result != PasswordVerificationResult.Success)
                return Unauthorized("Usuario o contraseña incorrectos.");

            // Generar el token JWT con información del rol
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    user.Id_User,
                    user.User_Name,
                    user.User_Contacto,
                    user.User_Access,
                    user.idRole,
                    role = user.role != null ? new
                    {
                        user.role.idRole,
                        user.role.roleNombre,
                        user.role.roleDescr,
                        user.role.canView,
                        user.role.canAdd,
                        user.role.canEdit,
                        user.role.canDelete,
                        user.role.canManageUsers,
                        user.role.canManageRoles,
                        user.role.canEvaluar,
                        user.role.canCContables
                    } : null
                }
            });
        }

        private string GenerateJwtToken(Users users) 
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["LlaveJWT"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, users.User_Access),
                new Claim("Id_User", users.Id_User.ToString()),
                new Claim("User_Name", users.User_Name),
                new Claim(ClaimTypes.Role, users.User_Rol)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
            

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id_User == id);
        }

        private async Task ReplicaUserNube(Users users, string metodo)
        {
            bool replicacionHabilitada = _configuration.GetValue<bool>("Replicacion:Habilitada");

            if (!replicacionHabilitada)
            {
                return;
            }
            try
            {
                var client = _httpClientFactory.CreateClient();
                string apiNubeUrlBase = _configuration.GetValue<string>("Replicacion:UrlApiNube");
                string apiNubeUrl = $"{apiNubeUrlBase}/Users";

                var jsonContent = JsonSerializer.Serialize(users);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{users.Id_User}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{users.Id_User}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar USERS en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción USERS al replicar en la nube: {ex.Message}");
            }
        }
    }
}
