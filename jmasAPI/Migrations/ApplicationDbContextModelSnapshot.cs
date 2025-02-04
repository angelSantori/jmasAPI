﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using jmasAPI;

#nullable disable

namespace jmasAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("jmasAPI.Models.AjustesMas", b =>
                {
                    b.Property<int>("Id_AjusteMas")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_AjusteMas"));

                    b.Property<string>("AjuesteMas_Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("AjusteMas_Cantidad")
                        .HasColumnType("double");

                    b.Property<string>("AjusteMas_Fecha")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<int>("Id_Producto")
                        .HasColumnType("int");

                    b.Property<int>("Id_User")
                        .HasColumnType("int");

                    b.HasKey("Id_AjusteMas");

                    b.HasIndex("Id_Producto");

                    b.HasIndex("Id_User");

                    b.ToTable("AjustesMas");
                });

            modelBuilder.Entity("jmasAPI.Models.AjustesMenos", b =>
                {
                    b.Property<int>("Id_AjusteMenos")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_AjusteMenos"));

                    b.Property<double>("AjusteMenos_Cantidad")
                        .HasColumnType("double");

                    b.Property<string>("AjusteMenos_Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("AjusteMenos_Fecha")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<int>("Id_Producto")
                        .HasColumnType("int");

                    b.Property<int>("Id_User")
                        .HasColumnType("int");

                    b.HasKey("Id_AjusteMenos");

                    b.HasIndex("Id_Producto");

                    b.HasIndex("Id_User");

                    b.ToTable("AjustesMenos");
                });

            modelBuilder.Entity("jmasAPI.Models.Almacenes", b =>
                {
                    b.Property<int>("Id_Almacen")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_Almacen"));

                    b.Property<string>("almacen_Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id_Almacen");

                    b.ToTable("Almacenes");
                });

            modelBuilder.Entity("jmasAPI.Models.Cancelado", b =>
                {
                    b.Property<int>("idCancelacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("idCancelacion"));

                    b.Property<int>("Id_Entrada")
                        .HasColumnType("int");

                    b.Property<int>("Id_User")
                        .HasColumnType("int");

                    b.Property<string>("cancelFecha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("cancelMotivo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("idCancelacion");

                    b.HasIndex("Id_Entrada");

                    b.HasIndex("Id_User");

                    b.ToTable("Cancelado");
                });

            modelBuilder.Entity("jmasAPI.Models.CapturaInvIni", b =>
                {
                    b.Property<int>("idInvIni")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("idInvIni"));

                    b.Property<int>("Id_Almacen")
                        .HasColumnType("int");

                    b.Property<int>("Id_Producto")
                        .HasColumnType("int");

                    b.Property<double>("invIniConteo")
                        .HasColumnType("double");

                    b.Property<string>("invIniFehca")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("idInvIni");

                    b.HasIndex("Id_Almacen");

                    b.HasIndex("Id_Producto");

                    b.ToTable("CapturaInvIni");
                });

            modelBuilder.Entity("jmasAPI.Models.Entradas", b =>
                {
                    b.Property<int>("Id_Entradas")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_Entradas"));

                    b.Property<string>("Entrada_CodFolio")
                        .HasColumnType("longtext");

                    b.Property<double>("Entrada_Costo")
                        .HasColumnType("double");

                    b.Property<bool>("Entrada_Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Entrada_Fecha")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Entrada_ImgB64Factura")
                        .HasColumnType("longtext");

                    b.Property<string>("Entrada_Referencia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Entrada_Unidades")
                        .HasColumnType("double");

                    b.Property<int>("Id_User")
                        .HasColumnType("int");

                    b.Property<int>("idProducto")
                        .HasColumnType("int");

                    b.HasKey("Id_Entradas");

                    b.HasIndex("Id_User");

                    b.HasIndex("idProducto");

                    b.ToTable("Entradas");
                });

            modelBuilder.Entity("jmasAPI.Models.Juntas", b =>
                {
                    b.Property<int>("Id_Junta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_Junta"));

                    b.Property<int>("Id_User")
                        .HasColumnType("int");

                    b.Property<string>("Junta_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Junta_Telefono")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id_Junta");

                    b.HasIndex("Id_User");

                    b.ToTable("Juntas");
                });

            modelBuilder.Entity("jmasAPI.Models.Padron", b =>
                {
                    b.Property<int>("idPadron")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("idPadron"));

                    b.Property<string>("padronDireccion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("padronNombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("idPadron");

                    b.ToTable("Padron");
                });

            modelBuilder.Entity("jmasAPI.Models.Productos", b =>
                {
                    b.Property<int>("Id_Producto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_Producto"));

                    b.Property<int>("idProveedor")
                        .HasColumnType("int");

                    b.Property<double>("prodCosto")
                        .HasColumnType("double");

                    b.Property<string>("prodDescripcion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<double>("prodExistencia")
                        .HasColumnType("double");

                    b.Property<string>("prodImgB64")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("prodMax")
                        .HasColumnType("double");

                    b.Property<double>("prodMin")
                        .HasColumnType("double");

                    b.Property<double>("prodPrecio")
                        .HasColumnType("double");

                    b.Property<string>("prodUMedEntrada")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("prodUMedSalida")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("prodUbFisica")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id_Producto");

                    b.HasIndex("idProveedor");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("jmasAPI.Models.Proveedores", b =>
                {
                    b.Property<int>("Id_Proveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_Proveedor"));

                    b.Property<string>("Proveedor_Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Proveedor_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Proveedor_Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id_Proveedor");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("jmasAPI.Models.Salidas", b =>
                {
                    b.Property<int>("Id_Salida")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_Salida"));

                    b.Property<int>("Id_Almacen")
                        .HasColumnType("int");

                    b.Property<int>("Id_Junta")
                        .HasColumnType("int");

                    b.Property<int>("Id_User")
                        .HasColumnType("int");

                    b.Property<string>("Salida_CodFolio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Salida_Costo")
                        .HasColumnType("double");

                    b.Property<string>("Salida_Fecha")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Salida_Referencia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Salida_Unidades")
                        .HasColumnType("double");

                    b.Property<int>("idProducto")
                        .HasColumnType("int");

                    b.HasKey("Id_Salida");

                    b.HasIndex("Id_Almacen");

                    b.HasIndex("Id_Junta");

                    b.HasIndex("Id_User");

                    b.HasIndex("idProducto");

                    b.ToTable("Salidas");
                });

            modelBuilder.Entity("jmasAPI.Models.Users", b =>
                {
                    b.Property<int>("Id_User")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_User"));

                    b.Property<string>("User_Access")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("User_Contacto")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("User_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("User_Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("User_Rol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id_User");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("jmasAPI.Models.AjustesMas", b =>
                {
                    b.HasOne("jmasAPI.Models.Productos", null)
                        .WithMany()
                        .HasForeignKey("Id_Producto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("Id_User")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("jmasAPI.Models.AjustesMenos", b =>
                {
                    b.HasOne("jmasAPI.Models.Productos", null)
                        .WithMany()
                        .HasForeignKey("Id_Producto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("Id_User")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("jmasAPI.Models.Cancelado", b =>
                {
                    b.HasOne("jmasAPI.Models.Entradas", null)
                        .WithMany()
                        .HasForeignKey("Id_Entrada")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("Id_User")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("jmasAPI.Models.CapturaInvIni", b =>
                {
                    b.HasOne("jmasAPI.Models.Almacenes", null)
                        .WithMany()
                        .HasForeignKey("Id_Almacen")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Productos", null)
                        .WithMany()
                        .HasForeignKey("Id_Producto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("jmasAPI.Models.Entradas", b =>
                {
                    b.HasOne("jmasAPI.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("Id_User")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Productos", null)
                        .WithMany()
                        .HasForeignKey("idProducto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("jmasAPI.Models.Juntas", b =>
                {
                    b.HasOne("jmasAPI.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("Id_User")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("jmasAPI.Models.Productos", b =>
                {
                    b.HasOne("jmasAPI.Models.Proveedores", null)
                        .WithMany()
                        .HasForeignKey("idProveedor")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("jmasAPI.Models.Salidas", b =>
                {
                    b.HasOne("jmasAPI.Models.Almacenes", null)
                        .WithMany()
                        .HasForeignKey("Id_Almacen")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Juntas", null)
                        .WithMany()
                        .HasForeignKey("Id_Junta")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("Id_User")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Productos", null)
                        .WithMany()
                        .HasForeignKey("idProducto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
