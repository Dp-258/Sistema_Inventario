﻿// <auto-generated />
using System;
using MVCInventario.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MVCInventario.Migrations
{
    [DbContext(typeof(MVCInventarioContext))]
    partial class MVCInventarioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVCInventario.Models.CLIENTE", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CEDULACLIENTE")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("NOMBRECLIENTE")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("CLIENTE");
                });

            modelBuilder.Entity("MVCInventario.Models.ENTRADA", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CANTIDADPENTRADA")
                        .HasColumnType("int");

                    b.Property<DateTime>("FECHAREGISTROENTRADA")
                        .HasColumnType("datetime2");

                    b.Property<int>("IDPRODUCTO")
                        .HasColumnType("int");

                    b.Property<int>("IDPROVEEDOR")
                        .HasColumnType("int");

                    b.Property<decimal>("MONTOTOTALENTRADA")
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("id");

                    b.ToTable("ENTRADA");
                });

            modelBuilder.Entity("MVCInventario.Models.PRODUCTO", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CATEGORIAPRODUCTO")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("CODIGOPRODUCTO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DESCRIPCIONPRODUCTO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FOTOPRODUCTO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NOMBREPRODUCTO")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<decimal>("PVPPRODUCTO")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int>("STOCKPRODUCTO")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PRODUCTO");
                });

            modelBuilder.Entity("MVCInventario.Models.PROVEEDOR", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CEDULAPROVEEDOR")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("CIUDADPROVEEDOR")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("CORREOPROVEEDOR")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("DIRECCIONPROVEEDOR")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("NOMBREPROVEEDOR")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("PROVEEDOR");
                });

            modelBuilder.Entity("MVCInventario.Models.SALIDA", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CANTIDADSALIDA")
                        .HasColumnType("int");

                    b.Property<DateTime>("FECHAREGISTROSALIDA")
                        .HasColumnType("datetime2");

                    b.Property<int>("IDCLIENTE")
                        .HasColumnType("int");

                    b.Property<int>("IDPRODUCTO")
                        .HasColumnType("int");

                    b.Property<decimal>("MONTOTOTALSALIDA")
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("id");

                    b.ToTable("SALIDA");
                });

            modelBuilder.Entity("MVCInventario.Models.USUARIO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NOMBREUSER")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ROLUSER")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("USUARIO");
                });
#pragma warning restore 612, 618
        }
    }
}
