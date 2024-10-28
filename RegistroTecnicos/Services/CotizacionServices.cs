using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Drawing;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class CotizacionServices(IDbContextFactory<Context> DbFactory)
{
    private readonly Context _context;

    public async Task<bool> Existe(int cotizacionId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.Cotizaciones.AnyAsync(c => c.CotizacionId == cotizacionId);
    }

    private async Task<bool> Insertar(Cotizaciones cotizaciones)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();

        foreach (var cotizacion in cotizaciones.cotizacionesDetalle)
        {
            var articulo = await BuscarArticulos(cotizacion.ArticuloId);

            if (articulo != null)
            {
                if (articulo.Existencia < cotizacion.Cantidad)
                {
                    return false;
                }
                articulo.Existencia -= cotizacion.Cantidad;
                _context.Articulos.Update(articulo);
            }
            else
            {

                return false;
            }
        }
        _context.Cotizaciones.Add(cotizaciones);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Cotizaciones cotizaciones)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        contexto.Update(cotizaciones);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Cotizaciones?> Buscar(int cotizacionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .Include(c => c.cotizacionesDetalle)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CotizacionId == cotizacionId);
    }

    public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Cotizaciones
            .Include(c => c.cotizacionesDetalle)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Articulos>> GetArticulos()
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.Articulos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Articulos> BuscarArticulos(int id)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.Articulos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ArticuloId == id);
    }

    public async Task<bool> ActualizarArticulo(Articulos articulo)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        _context.Articulos.Update(articulo);
        return await _context
            .SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Cotizaciones cotizaciones)
    {
        if (!await Existe(cotizaciones.CotizacionId))
        {
            return await Insertar(cotizaciones);
        }
        else
            return await Modificar(cotizaciones);
    }

    public async Task<List<CotizacionesDetalle>> BuscarTrabajoDetalle(int cotizacionId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        return await _context.cotizacionesDetalle
            .Include(a => a.Articulos)
            .Where(t => t.CotizacionId == cotizacionId)
             .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Eliminar(int cotizacionId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        var detalles = await BuscarTrabajoDetalle(cotizacionId);

        foreach (var detalle in detalles)
        {
            var articulo = await BuscarArticulos(detalle.ArticuloId);
            if (articulo != null)
            {
                articulo.Existencia += detalle.Cantidad;
                await ActualizarArticulo(articulo);
            }
        }
        var cobro = await _context.Cotizaciones
                    .Include(c => c.cotizacionesDetalle)
                    .FirstOrDefaultAsync(c => c.CotizacionId == cotizacionId);

        if (cobro == null) return false;

        _context.cotizacionesDetalle.RemoveRange(cobro.cotizacionesDetalle);
        _context.Cotizaciones.Remove(cobro);

        var cantidad = await _context.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<List<CotizacionesDetalle>> ListarTrabajoDetalle(int cotizacionId)
    {
        await using var _context = await DbFactory.CreateDbContextAsync();
        var cotizacionDetalle = await _context.cotizacionesDetalle
            .Where(d => d.CotizacionId == cotizacionId)
            .ToListAsync();

        return cotizacionDetalle;
    }
}
