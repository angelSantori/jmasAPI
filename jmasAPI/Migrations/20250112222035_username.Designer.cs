﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using jmasAPI;

#nullable disable

namespace jmasAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250112222035_username")]
    partial class username
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("AjuesteMenos_Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("AjusteMenos_Cantidad")
                        .HasColumnType("double");

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

            modelBuilder.Entity("jmasAPI.Models.Entidades", b =>
                {
                    b.Property<int>("Id_Entidad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_Entidad"));

                    b.Property<string>("Entidad_Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id_Entidad");

                    b.ToTable("Entidades");
                });

            modelBuilder.Entity("jmasAPI.Models.Entradas", b =>
                {
                    b.Property<int>("Id_Entradas")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_Entradas"));

                    b.Property<double>("Entrada_Costo")
                        .HasColumnType("double");

                    b.Property<string>("Entrada_Fecha")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Entrada_Folio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Entrada_Unidades")
                        .HasColumnType("double");

                    b.Property<int>("Id_Producto")
                        .HasColumnType("int");

                    b.Property<int>("Id_Proveedor")
                        .HasColumnType("int");

                    b.Property<int>("Id_User")
                        .HasColumnType("int");

                    b.Property<int>("User_Reporte")
                        .HasColumnType("int");

                    b.HasKey("Id_Entradas");

                    b.HasIndex("Id_Producto");

                    b.HasIndex("Id_Proveedor");

                    b.HasIndex("Id_User");

                    b.HasIndex("User_Reporte");

                    b.ToTable("Entradas");
                });

            modelBuilder.Entity("jmasAPI.Models.Juntas", b =>
                {
                    b.Property<int>("Id_Junta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_Junta"));

                    b.Property<string>("Junta_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id_Junta");

                    b.ToTable("Juntas");
                });

            modelBuilder.Entity("jmasAPI.Models.Productos", b =>
                {
                    b.Property<int>("Id_Producto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_Producto"));

                    b.Property<double>("Producto_Costo")
                        .HasColumnType("double");

                    b.Property<string>("Producto_Descripcion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Producto_Existencia")
                        .HasColumnType("double");

                    b.Property<double>("Producto_ExistenciaConFis")
                        .HasColumnType("double");

                    b.Property<double>("Producto_ExistenciaInicial")
                        .HasColumnType("double");

                    b.Property<string>("Producto_ImgBase64")
                        .HasColumnType("longtext");

                    b.Property<double?>("Producto_Precio1")
                        .HasColumnType("double");

                    b.Property<double?>("Producto_Precio2")
                        .HasColumnType("double");

                    b.Property<double?>("Producto_Precio3")
                        .HasColumnType("double");

                    b.Property<string>("Producto_UMedida")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id_Producto");

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

                    b.Property<int>("Id_Entidad")
                        .HasColumnType("int");

                    b.Property<int>("Id_Junta")
                        .HasColumnType("int");

                    b.Property<int>("Id_Producto")
                        .HasColumnType("int");

                    b.Property<int>("Id_Proveedor")
                        .HasColumnType("int");

                    b.Property<int>("Id_User")
                        .HasColumnType("int");

                    b.Property<double>("Salida_Costo")
                        .HasColumnType("double");

                    b.Property<string>("Salida_Fecha")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Salida_Folio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Salida_Unidades")
                        .HasColumnType("double");

                    b.Property<int>("User_Reporte")
                        .HasColumnType("int");

                    b.HasKey("Id_Salida");

                    b.HasIndex("Id_Entidad");

                    b.HasIndex("Id_Junta");

                    b.HasIndex("Id_Producto");

                    b.HasIndex("Id_Proveedor");

                    b.HasIndex("Id_User");

                    b.HasIndex("User_Reporte");

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

            modelBuilder.Entity("jmasAPI.Models.Entradas", b =>
                {
                    b.HasOne("jmasAPI.Models.Productos", null)
                        .WithMany()
                        .HasForeignKey("Id_Producto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Proveedores", null)
                        .WithMany()
                        .HasForeignKey("Id_Proveedor")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("Id_User")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("User_Reporte")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("jmasAPI.Models.Salidas", b =>
                {
                    b.HasOne("jmasAPI.Models.Entidades", null)
                        .WithMany()
                        .HasForeignKey("Id_Entidad")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Juntas", null)
                        .WithMany()
                        .HasForeignKey("Id_Junta")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Productos", null)
                        .WithMany()
                        .HasForeignKey("Id_Producto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Proveedores", null)
                        .WithMany()
                        .HasForeignKey("Id_Proveedor")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("Id_User")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("jmasAPI.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("User_Reporte")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
