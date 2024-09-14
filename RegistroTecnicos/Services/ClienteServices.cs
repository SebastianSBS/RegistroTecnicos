using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class ClienteServices
{
    private readonly Context _context;

    public ClienteServices(Context context)
    {
        _context = context;
    }

    public async Task<bool> Existe(int ClienteId)
    {
        return await _context.Clientes.AnyAsync(c => c.ClientesId == ClienteId);
    }

    public async Task<bool> Insertar(Clientes cliente)
    {
        _context.Clientes.Add(cliente);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Clientes cliente)
    {
        _context.Update(cliente);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Clientes cliente)
    {
        if (!await Existe(cliente.ClientesId))
            return await Insertar(cliente);
        else
            return await Modificar(cliente);
    }

    public async Task<bool> Eliminar(int ClienteId)
    {
        var cliente = await _context.Clientes.Where(c => c.ClientesId == ClienteId)
            .ExecuteDeleteAsync();
        return cliente > 0;
    }

    public async Task<Clientes?> Buscar(int ClienteId)
    {
        return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.ClientesId == ClienteId);
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        return await _context.Clientes.AsNoTracking().Where(criterio).ToListAsync();
    }
}
