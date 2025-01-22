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

namespace jmasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;        
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<Users> _passwordHasher;

        public UsersController(ApplicationDbContext context, IPasswordHasher<Users> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }        

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.Id_User)
            {
                return BadRequest();
            }

            var existingUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id_User == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Verifica si la contraseña fue modificada
            if (!string.IsNullOrEmpty(users.User_Password) && users.User_Password != existingUser.User_Password)
            {
                users.User_Password = _passwordHasher.HashPassword(users, users.User_Password);
            }
            else
            {
                // Si no se modificó, usa la contraseña existente
                users.User_Password = existingUser.User_Password;
            }            

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            //Unicidad de User_Contacto y User_Access
            if (await _context.Users.AnyAsync(uContacto => uContacto.User_Contacto == users.User_Contacto))
            {
                return Conflict("El contaco ya está en uso");
            }

            if (await _context.Users.AnyAsync(uAccess => uAccess.User_Access == users.User_Access))
            {
                return Conflict("Palabra de acceso ya está en uso");
            }

            //Hash
            users.User_Password = _passwordHasher.HashPassword(users, users.User_Password);

            _context.Users.Add(users);            
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUsers", new { id = users.Id_User }, users);
        }

        //Inicio de sesión
        //Login
        public class LoginRequest
        {
            public string UserAccess { get; set; }
            public string UserPassword { get; set; }
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Access == loginRequest.UserAccess);
            if (user == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            //Validar contraseña
            var result = _passwordHasher.VerifyHashedPassword(user, user.User_Password, loginRequest.UserPassword);
            if (result != PasswordVerificationResult.Success)
                return Unauthorized("Usuario o contraseña incorrectos.");

            //Generar el token JWT
            var token = GenerateJwtToken(user);
            return Ok(new { token });
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
    }
}
