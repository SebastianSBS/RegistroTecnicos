using global::RegistroTecnicos.DAL;
using global::RegistroTecnicos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class PrioridadesServices(IDbContextFactory<Context> DbFactory)

{
    private readonly Context _context;

    public async Task<bool> Existe(int prioridadId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.Prioridades.AnyAsync(t => t.PrioridadId == prioridadId);
    }

    public async Task<bool> Insertar(Prioridades prioridades)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        _context.Prioridades.Add(prioridades);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Prioridades prioridades)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
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
        await using var _context = await DbFactory.CreateDbContextAsync();
        var prioridad = await _context.Prioridades
    //                .Include(c => c.PrioridadId)
                    .FirstOrDefaultAsync(c => c.PrioridadId == prioridadId);
        if (prioridad == null) return false;
        _context.Prioridades.Remove(prioridad);
        var cantidad = await _context.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<Prioridades?> Buscar(int prioridadId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.Prioridades
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.PrioridadId == prioridadId);
    }

    public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.Prioridades
            .Where(criterio)
            .AsNoTracking()   
            .ToListAsync();
    }

    public async Task<List<Prioridades>> GetPrioridades()
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.Prioridades.ToListAsync();
    }
}
