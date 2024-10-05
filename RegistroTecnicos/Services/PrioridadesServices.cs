using global::RegistroTecnicos.DAL;
using global::RegistroTecnicos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class PrioridadesServices
{
    private readonly Context _context;

    public PrioridadesServices(Context context)
    {
        _context = context;
    }

    public async Task<bool> Existe(int prioridadId)
    {
        return await _context.Priodades.AnyAsync(t => t.PrioridadId == prioridadId);
    }

    public async Task<bool> Insertar(Prioridades prioridades)
    {
        _context.Priodades.Add(prioridades);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Prioridades prioridades)
    {
        _context.Update(prioridades);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Prioridades prioridades)
    {
        if (!await Existe(prioridades.PrioridadId))
            return await Insertar(prioridades);
        else
            return await Modificar(prioridades);
    }

    public async Task<bool> Eliminar(int prioridadId)
    {
        var prioridades = await _context.Priodades.Where(t => t.PrioridadId == prioridadId)
            .ExecuteDeleteAsync();
        return prioridades > 0;
    }

    public async Task<Prioridades?> Buscar(int prioridadId)
    {
        return await _context.Priodades.AsNoTracking().FirstOrDefaultAsync(t => t.PrioridadId == prioridadId);
    }

    public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
    {
        return await _context.Priodades.AsNoTracking().Where(criterio).ToListAsync();
    }

    public async Task<List<Prioridades>> GetPrioridades()
    {
        return await _context.Priodades.ToListAsync();
    }
}
