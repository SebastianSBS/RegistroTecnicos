using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TiposTecnicoServices
{ 
    private readonly Context _context;

    public TiposTecnicoServices(Context context)
    {
        _context = context;
    }

    public async Task<bool> Existe(int TipoTecnicoId)
    {
        return await _context.TiposTecnicos.AnyAsync(t => t.TipoTecnicoId == TipoTecnicoId);
    }

    public async Task<bool> Insertar(TiposTecnico tipoTecnicos)
    {
        _context.TiposTecnicos.Add(tipoTecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(TiposTecnico tipoTecnicos)
    {
        _context.Update(tipoTecnicos);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(TiposTecnico tipoTecnicos)
    {
        if (!await Existe(tipoTecnicos.TipoTecnicoId))
            return await Insertar(tipoTecnicos);
        else
            return await Modificar(tipoTecnicos);
    }

    public async Task<bool> Eliminar(int tipoTecnicoId)
    {
        var tipoTecnicos = await _context.TiposTecnicos.Where(t => t.TipoTecnicoId == tipoTecnicoId)
            .ExecuteDeleteAsync();
        return tipoTecnicoId > 0;
    }

    public async Task<TiposTecnico?> Buscar(int tipoTecnicoId)
    {
        return await _context.TiposTecnicos.AsNoTracking().FirstOrDefaultAsync(t => t.TipoTecnicoId == tipoTecnicoId);
    }

    public async Task<List<TiposTecnico>> Listar(Expression<Func<TiposTecnico, bool>> criterio)
    {
        return await _context.TiposTecnicos.AsNoTracking().Where(criterio).ToListAsync();
    }
}