using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public DbSet<CContable> CContable { get; set; } = default!;        

        public DbSet<CanceladoSalida> CanceladoSalida { get; set; } = default!;

        public DbSet<LectEnviar> LectEnviar { get; set; } = default!;

        public DbSet<ProblemasLectura> ProblemasLectura { get; set; } = default!;

        public DbSet<Calle> Calle { get; set; } = default!;

        public DbSet<Colonia> Colonia { get; set; } = default!;

        public DbSet<Herramienta> Herramienta { get; set; } = default!;

        public DbSet<htaPrestamo> htaPrestamo { get; set; } = default!;

        public DbSet<Role> Role { get; set; } = default!;

        public DbSet<DocumentPdf> documentPdf { get; set; } = default!;

        public DbSet<OrdenServicio> ordenServicio { get; set; } = default!;

        public DbSet<EvaluacionOrdenServicio> evaluacionOrdenServicio { get; set; } = default!;

        public DbSet<TrabajoRealizado> trabajoRealizado { get; set; } = default!;

        public DbSet<OrdenCompra> ordenCompra { get; set; } = default!;

        public DbSet<TipoProblema> tipoProblema { get; set; } = default!;

        public DbSet<MedioOrdenServicio> medioOrdenServicio { get; set; } = default!;

        public DbSet<EntrevistaPadron> entrevistaPadron { get; set; } = default!;

        public DbSet<Asistencia> asistencias { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //  Asistencias
            modelBuilder.Entity<Asistencia>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(userAsistencia => userAsistencia.idUser)
                .OnDelete(DeleteBehavior.Restrict);

            //  Entrevista Padron
            modelBuilder.Entity<EntrevistaPadron>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(enntrevistaPadronUser =>  enntrevistaPadronUser.idUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EntrevistaPadron>()
                .HasOne<OrdenServicio>() 
                .WithMany()
                .HasForeignKey(entrevistaPadronOS =>  entrevistaPadronOS.idOrdenServicio)
                .OnDelete(DeleteBehavior.Restrict);
            
            //Orden Compra
            modelBuilder.Entity<OrdenCompra>()
                .HasOne<Proveedores>()
                .WithMany()
                .HasForeignKey(provOC => provOC.idProveedor)
                .OnDelete(DeleteBehavior.Restrict);

            //Trabajo Realizado
            modelBuilder.Entity<TrabajoRealizado>()
                .HasOne<OrdenServicio>()
                .WithMany()
                .HasForeignKey(ordenTR => ordenTR.idOrdenServicio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrabajoRealizado>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(userTR => userTR.idUserTR)
                .OnDelete(DeleteBehavior.Restrict);

            //Evaluacion orden trabajo
            modelBuilder.Entity<EvaluacionOrdenServicio>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(userEOT => userEOT.idUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EvaluacionOrdenServicio>()
                .HasOne<OrdenServicio>() 
                .WithMany()
                .HasForeignKey(ordenTrabajoEOS => ordenTrabajoEOS.idOrdenServicio) 
                .OnDelete(DeleteBehavior.Restrict);

            //Orden Servicio
            modelBuilder.Entity<OrdenServicio>()
                .HasOne<MedioOrdenServicio>()
                .WithMany()
                .HasForeignKey(medioOS => medioOS.idMedio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrdenServicio>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(userOS => userOS.idUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrdenServicio>()
                .HasOne<Padron>()
                .WithMany()
                .HasForeignKey(padronOS => padronOS.idPadron)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrdenServicio>()
                .HasOne<TipoProblema>()
                .WithMany()
                .HasForeignKey(osTP => osTP.idTipoProblema)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrdenServicio>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(userAsignadoOS => userAsignadoOS.idUserAsignado)
                .OnDelete(DeleteBehavior.Restrict);

            //PDF
            modelBuilder.Entity<DocumentPdf>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(docUser => docUser.idUser)
                .OnDelete(DeleteBehavior.Restrict);

            //User
            modelBuilder.Entity<Users>()
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey(user => user.idRole)
                .OnDelete(DeleteBehavior.Restrict);

            //htaPrestamo
            modelBuilder.Entity<htaPrestamo>()
                .HasOne<Herramienta>()
                .WithMany()
                .HasForeignKey(prestamo => prestamo.idHerramienta)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<htaPrestamo>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(prestamo => prestamo.Id_UserAsignado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<htaPrestamo>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(presUserRes => presUserRes.idUserResponsable)
                .OnDelete(DeleteBehavior.Restrict);
            
            //LECTURAS
            modelBuilder.Entity<LectEnviar>()
                .HasOne<ProblemasLectura>()
                .WithMany()
                .HasForeignKey(lectProb => lectProb.idProblema)
                .OnDelete(DeleteBehavior.Restrict);

            //PRODUCTOS relación
            modelBuilder.Entity<Productos>()
                .HasOne<Proveedores>()
                .WithMany()
                .HasForeignKey(prodProv => prodProv.idProveedor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Productos>()
                .HasOne<Almacenes>()
                .WithMany()
                .HasForeignKey(prodAlm => prodAlm.Id_Almacen)
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

            modelBuilder.Entity<Salidas>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(salUserAsig => salUserAsig.Id_User_Asignado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Salidas>()
                .HasOne<Padron>()
                .WithMany()
                .HasForeignKey(salPadron => salPadron.idPadron)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Salidas>()
                .HasOne<Calle>()
                .WithMany()
                .HasForeignKey(salCalle => salCalle.idCalle)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Salidas>()
                .HasOne<Colonia>()
                .WithMany()
                .HasForeignKey(salColonia  => salColonia.idColonia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Salidas>()
                .HasOne<OrdenServicio>()
                .WithMany()
                .HasForeignKey(salOT => salOT.idOrdenServicio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Salidas>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(salUserAutoriza => salUserAutoriza.idUserAutoriza)
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
            
            modelBuilder.Entity<Entradas>()
                .HasOne<Almacenes>()
                .WithMany()
                .HasForeignKey(entAlm => entAlm.Id_Almacen)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entradas>()
                .HasOne<Proveedores>()
                .WithMany()
                .HasForeignKey(entrProve => entrProve.Id_Proveedor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entradas>()
                .HasOne<Juntas>()
                .WithMany()
                .HasForeignKey(entrJun => entrJun.Id_Junta)
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

            //Cancelado Salida
            modelBuilder.Entity<CanceladoSalida>()
                .HasOne<Salidas>()
                .WithMany()
                .HasForeignKey(cancelSalidaSalida => cancelSalidaSalida.Id_Salida)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CanceladoSalida>()
                .HasOne<Users>()
                .WithMany()
                .HasForeignKey(cancelSalidaUser => cancelSalidaUser.Id_User)
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

            //CContable
            modelBuilder.Entity<CContable>()
                .HasOne<Productos>()
                .WithMany()
                .HasForeignKey(ccontableProd => ccontableProd.idProducto)
                .OnDelete(DeleteBehavior.Restrict);
        }        
    }
}
