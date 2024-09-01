using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TecnicoServices
    {
        private readonly Context _context;

        public TecnicoServices(Context context) 
        {
            _context = context;
        }

        public async Task <bool> Existe(int TecnicoId)
        {
            return await _context.Tecnicos.AnyAsync(t => t.TecnicoId == TecnicoId);
        }

        public async Task<bool> Insertar(Tecnicos tecnicos)
        {
            _context.Tecnicos.Add(tecnicos);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Tecnicos tecnicos)
        {
            _context.Update(tecnicos);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Tecnicos tecnicos)
        {
            if (!await Existe(tecnicos.TecnicoId))
                return await Insertar(tecnicos);
            else
                return await Modificar(tecnicos);
        }

        public async Task<bool> Eliminar(int TecnicoId)
        {
            var tecnicos = await _context.Tecnicos.Where(t => t.TecnicoId == TecnicoId)
                .ExecuteDeleteAsync();
            return tecnicos > 0;
        }

        public async Task<Tecnicos?> Buscar(int TecnicoId)
        {
            return await _context.Tecnicos.AsNoTracking().FirstOrDefaultAsync(t => t.TecnicoId == TecnicoId);
        }

        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            return await _context.Tecnicos.AsNoTracking().Where(criterio).ToListAsync();
        }
    }
}
