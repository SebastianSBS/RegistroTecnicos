using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TrabajoServices
{
    private readonly Context _context;

    public TrabajoServices(Context context)
    {
        _context = context;
    }

    public async Task<bool> Existe(int TrabajoId)
    {
        return await _context.Trabajos.AnyAsync(t => t.TrabajoId == TrabajoId);
    }

    public async Task<bool> Insertar(Trabajos trabajo)
    {
        _context.Trabajos.Add(trabajo);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Trabajos trabajo)
    {
        _context.Update(trabajo);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Trabajos trabajos)
    {
        if (!await Existe(trabajos.TrabajoId))
            return await Insertar(trabajos);
        else
            return await Modificar(trabajos);
    }

    public async Task<bool> Eliminar(int TrabajoId)
    {
        var trabajo = await _context.Trabajos.Where(t => t.TrabajoId == TrabajoId)
            .ExecuteDeleteAsync();
        return trabajo > 0;
    }

    public async Task<Trabajos?> Buscar(int TrabajoId)
    {
        return await _context.Trabajos.AsNoTracking().FirstOrDefaultAsync(t => t.TrabajoId == TrabajoId);
    }

    public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
    {
        return await _context.Trabajos.AsNoTracking().Where(criterio).ToListAsync();
    }
}
