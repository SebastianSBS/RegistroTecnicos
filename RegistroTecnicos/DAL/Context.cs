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

    public DbSet<Prioridades> Priodades { get; set; }

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
    }
}
