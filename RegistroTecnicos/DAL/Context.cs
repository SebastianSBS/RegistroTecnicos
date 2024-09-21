using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.DAL;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) 
        : base(options) { }

    public DbSet<Tecnicos> Tecnicos { get; set; }

    public DbSet<TiposTecnico> TiposTecnicos { get; set; }

    public DbSet<Clientes> Clientes { get; set; }

    public DbSet<Trabajos> Trabajos { get; set; }

    public DbSet<Prioridades> Priodades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TiposTecnico>().HasData
        (
            new TiposTecnico
            {
                TipoTecnicoId = 1,
                Descripcion = "Redes",
            },

            new TiposTecnico
            {
                TipoTecnicoId = 2,
                Descripcion = "Reparacion",

            },

            new TiposTecnico
            {
                TipoTecnicoId = 3,
                Descripcion = "Impresoras",

            },

            new TiposTecnico
            {
                TipoTecnicoId = 4,
                Descripcion = "Soporte",

            },

            new TiposTecnico
            {
                TipoTecnicoId = 5,
                Descripcion = "Celulares",
            }

        );
    }
}
