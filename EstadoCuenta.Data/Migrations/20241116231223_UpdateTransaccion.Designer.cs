﻿// <auto-generated />
using System;
using EstadoCuenta.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EstadoCuenta.Data.Migrations
{
    [DbContext(typeof(EstadoCuentaContext))]
    [Migration("20241116231223_UpdateTransaccion")]
    partial class UpdateTransaccion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EstadoCuenta.Data.Models.Tarjeta", b =>
                {
                    b.Property<string>("NumTarjeta")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdTarjeta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTarjeta"), 1L, 1);

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<decimal>("LimiteCredito")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SaldoDisponible")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("NumTarjeta");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Tarjeta", (string)null);
                });

            modelBuilder.Entity("EstadoCuenta.Data.Models.TipoTransaccion", b =>
                {
                    b.Property<int>("IdTipoTransaccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTipoTransaccion"), 1L, 1);

                    b.Property<string>("TipoDeTransaccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTipoTransaccion");

                    b.ToTable("TipoTransaccion", (string)null);
                });

            modelBuilder.Entity("EstadoCuenta.Data.Models.Transaccion", b =>
                {
                    b.Property<int>("IdTransaccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTransaccion"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdTipoTransaccion")
                        .HasColumnType("int");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NumTarjeta")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal?>("SaldoDisponible")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdTransaccion");

                    b.HasIndex("IdTipoTransaccion");

                    b.HasIndex("NumTarjeta");

                    b.ToTable("Transaccion", (string)null);
                });

            modelBuilder.Entity("EstadoCuenta.Data.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"), 1L, 1);

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dui")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("EstadoCuenta.Data.Models.Tarjeta", b =>
                {
                    b.HasOne("EstadoCuenta.Data.Models.Usuario", "Usuario")
                        .WithMany("Tarjetas")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("EstadoCuenta.Data.Models.Transaccion", b =>
                {
                    b.HasOne("EstadoCuenta.Data.Models.TipoTransaccion", "TipoTransacciones")
                        .WithMany("Transacciones")
                        .HasForeignKey("IdTipoTransaccion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EstadoCuenta.Data.Models.Tarjeta", "Tarjetas")
                        .WithMany("Transacciones")
                        .HasForeignKey("NumTarjeta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tarjetas");

                    b.Navigation("TipoTransacciones");
                });

            modelBuilder.Entity("EstadoCuenta.Data.Models.Tarjeta", b =>
                {
                    b.Navigation("Transacciones");
                });

            modelBuilder.Entity("EstadoCuenta.Data.Models.TipoTransaccion", b =>
                {
                    b.Navigation("Transacciones");
                });

            modelBuilder.Entity("EstadoCuenta.Data.Models.Usuario", b =>
                {
                    b.Navigation("Tarjetas");
                });
#pragma warning restore 612, 618
        }
    }
}
