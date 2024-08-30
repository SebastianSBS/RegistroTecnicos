using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) 
            : base(options) { }

        public DbSet<Tecnicos> Tecnicos { get; set; }
    }
}
