﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using jmasAPI.Models;

namespace jmasAPI
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<jmasAPI.Models.Users> Users { get; set; } = default!;

        public DbSet<Productos> Productos { get; set; } = default!;

        public DbSet<Salidas> Salidas { get; set; } = default!;

        public DbSet<Proveedores> Proveedores { get; set; } = default!;

        public DbSet<Entradas> Entradas { get; set; } = default!;

        public DbSet<AjustesMas> AjustesMas { get; set; } = default!;

        public DbSet<AjustesMenos> AjustesMenos { get; set; } = default!;        

        public DbSet<Almacenes> Almacenes { get; set; } = default!;

        public DbSet<Juntas> Juntas { get; set; } = default!;

        public DbSet<Cancelado> Cancelado { get; set; } = default!;

        public DbSet<CapturaInvIni> CapturaInvIni { get; set; } = default!;

        public DbSet<Padron> Padron { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //PRODUCTOS relación
            modelBuilder.Entity<Productos>()
                .HasOne<Proveedores>()
                .WithMany()
                .HasForeignKey(prodProv => prodProv.idProveedor)
                .OnDelete(DeleteBehavior.Restrict);

            //SALIDAS relaciones
            modelBuilder.Entity<Salidas>()
                .HasOne<Productos>()
                .WithMany()
                .HasForeignKey(salProd => salProd.idProducto)
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
                .HasOne<Almacenes>()
                .WithMany()
                .HasForeignKey(salAlma => salAlma.Id_Almacen)
                .OnDelete(DeleteBehavior.Restrict);

            //ENTRADAS relaciones
            modelBuilder .Entity<Entradas>()
                .HasOne<Productos>()
                .WithMany()
                .HasForeignKey(entProd => entProd.idProducto)
                .OnDelete(DeleteBehavior.Restrict);            

            modelBuilder.Entity<Entradas>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(entUser => entUser.Id_User)
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
            
            //Juntas
            modelBuilder.Entity<Juntas>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(juntUser => juntUser.Id_User)
                .OnDelete(DeleteBehavior.Restrict);

            //Cancelado
            modelBuilder.Entity<Cancelado>()
                .HasOne<Entradas>()
                .WithMany() 
                .HasForeignKey(cancelEntrada => cancelEntrada.Id_Entrada)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cancelado>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(cancelUser => cancelUser.Id_User)
                .OnDelete(DeleteBehavior.Restrict);

            //CapturaInvIni
            modelBuilder.Entity<CapturaInvIni>()
                .HasOne<Productos>()
                .WithMany()
                .HasForeignKey(invInProd => invInProd.Id_Producto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CapturaInvIni>()
                .HasOne<Almacenes>()
                .WithMany()
                .HasForeignKey(invInAlm => invInAlm.Id_Almacen)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
