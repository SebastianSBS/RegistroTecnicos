using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.DAL;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) 
        : base(options) { }

    public DbSet<Tecnicos> Tecnicos { get; set; }
    public DbSet<TiposTecnicos> TiposTecnicos { get; set; }

    public DbSet<Clientes> Clientes { get; set; }

    public DbSet<Trabajos> Trabajos { get; set; }

    public DbSet<Prioridades> Prioridades { get; set; }

    public DbSet<Articulos> Articulos { get; set; }

    public DbSet<TrabajosDetalle> trabajosDetalles { get; set; }

    public DbSet<Cotizaciones> Cotizaciones { get; set; }

    public DbSet<CotizacionesDetalle> cotizacionesDetalle { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TiposTecnicos>().HasData
        (
            new TiposTecnicos
            {
                TipoTecnicoId = 1,
                Descripcion = "Redes",
            },

            new TiposTecnicos
            {
                TipoTecnicoId = 2,
                Descripcion = "Reparacion",

            },

            new TiposTecnicos
            {
                TipoTecnicoId = 3,
                Descripcion = "Impresoras",

            },

            new TiposTecnicos
            {
                TipoTecnicoId = 4,
                Descripcion = "Soporte",

            },

            new TiposTecnicos
            {
                TipoTecnicoId = 5,
                Descripcion = "Celulares",
            }
        );

        modelBuilder.Entity<Articulos>().HasData
        (
              new Articulos
              {
                  ArticuloId = 1,
                  Descripcion = "Pasta termica",
                  Costo = 350,
                  Precio = 400,
                  Existencia = 10,
              },

              new Articulos
              {
                  ArticuloId = 2,
                  Descripcion = "HDD SEAGATE 250GB",
                  Costo = 800,
                  Precio = 970,
                  Existencia = 5,
              },

              new Articulos
              {
                  ArticuloId = 3,
                  Descripcion = "Memoria RAM 2 GB DDR3",
                  Costo = 600,
                  Precio = 850,
                  Existencia = 15,
              },

              new Articulos
              {
                  ArticuloId = 4,
                  Descripcion = "Tarjeta de sonido Sound blaster 5/RX",
                  Costo = 750,
                  Precio = 1200,
                  Existencia = 4,
              },

              new Articulos
              {
                  ArticuloId = 5,
                  Descripcion =  "Cable SATA de datos",
                  Costo = 80, 
                  Precio = 100,
                  Existencia = 30,
              }
        );
    }
}
