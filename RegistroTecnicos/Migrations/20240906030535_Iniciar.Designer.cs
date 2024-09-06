﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistroTecnicos.DAL;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240906030535_Iniciar")]
    partial class Iniciar
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("RegistroTecnicos.Models.Tecnicos", b =>
                {
                    b.Property<int>("TecnicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SueldoHora")
                        .HasColumnType("INTEGER");

                    b.HasKey("TecnicoId");

                    b.ToTable("Tecnicos");
                });

            modelBuilder.Entity("RegistroTecnicos.Models.TiposTecnico", b =>
                {
                    b.Property<int>("TipoTecnicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TecnicoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TipoTecnicoId");

                    b.ToTable("TiposTecnicos");

                    b.HasData(
                        new
                        {
                            TipoTecnicoId = 1,
                            Descripcion = "Redes",
                            TecnicoId = 0
                        },
                        new
                        {
                            TipoTecnicoId = 2,
                            Descripcion = "Reparacion",
                            TecnicoId = 0
                        },
                        new
                        {
                            TipoTecnicoId = 3,
                            Descripcion = "Impresoras",
                            TecnicoId = 0
                        },
                        new
                        {
                            TipoTecnicoId = 4,
                            Descripcion = "Soporte",
                            TecnicoId = 0
                        },
                        new
                        {
                            TipoTecnicoId = 5,
                            Descripcion = "Celulares",
                            TecnicoId = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}