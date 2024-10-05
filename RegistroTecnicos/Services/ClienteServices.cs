﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> Existe(int clienteId)
    {
        return await _context.Clientes.AnyAsync(c => c.ClienteId == clienteId);
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
        if (!await Existe(cliente.ClienteId))
            return await Insertar(cliente);
        else
            return await Modificar(cliente);
    }

    public async Task<bool> Eliminar(int clienteId)
    {
        var cliente = await _context.Clientes.Where(c => c.ClienteId == clienteId)
            .ExecuteDeleteAsync();
        return cliente > 0;
    }

    public async Task<Clientes?> Buscar(int clienteId)
    {
        return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.ClienteId == clienteId);
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        return await _context.Clientes.AsNoTracking().Where(criterio).ToListAsync();
    }

    public async Task<List<Clientes>> GetClientes ()
    {
        return await _context.Clientes.ToListAsync();
    }
}
