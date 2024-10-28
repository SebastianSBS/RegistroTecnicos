using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TiposTecnicoServices(IDbContextFactory<Context> DbFactory)
{
    private readonly Context _context;
    public async Task<bool> Existe(int tipoTecnicoId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.TiposTecnicos.AnyAsync(t => t.TipoTecnicoId == tipoTecnicoId);
    }

    public async Task<bool> Insertar(TiposTecnicos tipoTecnicos)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        _context.TiposTecnicos.Add(tipoTecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(TiposTecnicos tipoTecnicos)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        _context.Update(tipoTecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(TiposTecnicos tipoTecnicos)
    {
        if (!await Existe(tipoTecnicos.TipoTecnicoId))
            return await Insertar(tipoTecnicos);
        else
            return await Modificar(tipoTecnicos);
    }

    public async Task<bool> Eliminar(int tipoTecnicoId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        var tipoTecnicos = await _context.TiposTecnicos.Where(t => t.TipoTecnicoId == tipoTecnicoId)
            .ExecuteDeleteAsync();
        return tipoTecnicoId > 0;
    }

    public async Task<TiposTecnicos?> Buscar(int tipoTecnicoId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.TiposTecnicos
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.TipoTecnicoId == tipoTecnicoId);
    }

    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.TiposTecnicos
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<TiposTecnicos>> GetTipoTecnicos()
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.TiposTecnicos
            .AsNoTracking()
            .ToListAsync();
    }
}