using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicoServices(IDbContextFactory<Context> DbFactory)
{
    private readonly Context _context;

    public async Task <bool> Existe(int tecnicoId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.Tecnicos.AnyAsync(t => t.TecnicoId == tecnicoId);
    }

    public async Task<bool> Insertar(Tecnicos tecnicos)
    {

        await using var _context = await DbFactory.CreateDbContextAsync();
        _context.Tecnicos.Add(tecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Tecnicos tecnicos)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
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

    public async Task<bool> Eliminar(int tecnicoId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        var tecnicos = await _context.Tecnicos.Where(t => t.TecnicoId == tecnicoId)
            .ExecuteDeleteAsync();
        return tecnicos > 0;
    }

    public async Task<Tecnicos?> Buscar(int tecnicoId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.Tecnicos.AsNoTracking()
            .FirstOrDefaultAsync(t => t.TecnicoId == tecnicoId);
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.Tecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<List<Tecnicos>> GetTecnicos()
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.Tecnicos
            .AsNoTracking()
            .ToListAsync();
    }
}
