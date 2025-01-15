using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using jmasAPI.Models;

namespace jmasAPI
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<jmasAPI.Models.Users> Users { get; set; } = default!;

        public DbSet<jmasAPI.Models.Productos> Productos { get; set; } = default!;

        public DbSet<Salidas> Salidas { get; set; } = default!;

        public DbSet<Proveedores> Proveedores { get; set; } = default!;

        public DbSet<Entradas> Entradas { get; set; } = default!;

        public DbSet<AjustesMas> AjustesMas { get; set; } = default!;

        public DbSet<AjustesMenos> AjustesMenos { get; set; } = default!;

        public DbSet<Entidades> Entidades { get; set; } = default!;

        public DbSet<Juntas> Juntas { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //SALIDAS relaciones
            modelBuilder.Entity<Salidas>()
                .HasOne<Productos>()
                .WithMany()
                .HasForeignKey(salProd => salProd.Id_Producto)
                .OnDelete(DeleteBehavior.Restrict);            

            modelBuilder.Entity<Salidas>()
                .HasOne<Proveedores>()
                .WithMany()
                .HasForeignKey(salProv => salProv.Id_Proveedor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Salidas>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(salUser => salUser.Id_User)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Salidas>()
                .HasOne<Juntas>()
                .WithMany()
                .HasForeignKey(salJunta => salJunta.Id_Junta)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Salidas>()
                .HasOne<Entidades>()
                .WithMany()
                .HasForeignKey(salEntidad => salEntidad.Id_Entidad)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Salidas>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(salUserReport => salUserReport.User_Reporte)
                .OnDelete(DeleteBehavior.Restrict);

            //ENTRADAS relaciones
            modelBuilder .Entity<Entradas>()
                .HasOne<Productos>()
                .WithMany()
                .HasForeignKey(entProd => entProd.Id_Producto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entradas>()
                .HasOne<Proveedores>()
                .WithMany()
                .HasForeignKey(entProv => entProv.Id_Proveedor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entradas>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(entUser => entUser.Id_User)
                .OnDelete(DeleteBehavior.Restrict);            

            modelBuilder.Entity<Entradas>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(entUserReport => entUserReport.User_Reporte)
                .OnDelete(DeleteBehavior.Restrict);

            //AjustesMas relaciones
            modelBuilder.Entity<AjustesMas>()
                .HasOne<Productos>()
                .WithMany()
                .HasForeignKey(ajMasProd => ajMasProd.Id_Producto)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<AjustesMas>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(ajMasUser => ajMasUser.Id_User)
                .OnDelete(DeleteBehavior.Restrict);

            //AjusteMenos relaciones
            modelBuilder.Entity<AjustesMenos>()
                .HasOne<Productos>()
                .WithMany()
                .HasForeignKey(ajMenProd => ajMenProd.Id_Producto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AjustesMenos>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(ajMenUser => ajMenUser.Id_User)
                .OnDelete(DeleteBehavior.Restrict);
        }                
    }
}
