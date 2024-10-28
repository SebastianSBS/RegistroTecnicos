using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Drawing;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TrabajoServices(IDbContextFactory<Context> DbFactory)
    {
        private readonly Context _context;
        public async Task<bool> Existe(int trabajoId)
        {
            await using var _context = await DbFactory.CreateDbContextAsync();
            return await _context.Trabajos.AnyAsync(t => t.TrabajoId == trabajoId);
        }

        public async Task<bool> Insertar(Trabajos trabajo)
        {
            await using var _context = await DbFactory.CreateDbContextAsync();
            foreach (var detalle in trabajo.trabajosDetalle)
            {
                var articulo = await BuscarArticulos(detalle.ArticuloId);

                if (articulo != null)
                {
                    if (articulo.Existencia < detalle.Cantidad)
                    {
                        return false;
                    }
                    articulo.Existencia -= detalle.Cantidad;
                    _context.Articulos.Update(articulo);
                }
                else
                {
                    return false;
                }
            }
            _context.Trabajos.Add(trabajo);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Trabajos trabajo)
        {
            await using var _context = await DbFactory.CreateDbContextAsync();
            _context.Update(trabajo);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Trabajos?> Buscar(int trabajoId)
        {
            await using var _context = await DbFactory.CreateDbContextAsync();
            return await _context.Trabajos
                .Include(t => t.trabajosDetalle)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TrabajoId == trabajoId);
        }

        public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
        {
            await using var _context = await DbFactory.CreateDbContextAsync();
            return await _context.Trabajos
                .Include(t => t.trabajosDetalle)
                .AsNoTracking()
                .Where(criterio)
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

        public async Task<bool> Guardar(Trabajos trabajo)
        {
            if (!await Existe(trabajo.TrabajoId))
            { 
                return await Insertar(trabajo);
            }
            else
                return await Modificar(trabajo);
        }

        public async Task<List<TrabajosDetalle>> BuscarTrabajoDetalle(int trabajoId)
        {
            await using var _context = await DbFactory.CreateDbContextAsync();
            return await _context.trabajosDetalles
                .Include(a => a.Articulos)
                .Where(t => t.TrabajoId == trabajoId)
                 .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Eliminar(int trabajoId)
        {
            await using var _context = await DbFactory.CreateDbContextAsync();
            var detalles = await BuscarTrabajoDetalle(trabajoId);

            foreach (var detalle in detalles)
            {
                var articulo = await BuscarArticulos(detalle.ArticuloId);
                if (articulo != null)
                {
                    articulo.Existencia += detalle.Cantidad; 
                    await ActualizarArticulo(articulo);
                }
            }
            var cobro = await _context.Trabajos
                   .Include(c => c.trabajosDetalle)
                   .FirstOrDefaultAsync(t => t.TrabajoId == trabajoId);
            if (cobro == null) return false;

            _context.trabajosDetalles.RemoveRange(cobro.trabajosDetalle);
            _context.Trabajos.Remove(cobro);
            var cantidad = await _context.SaveChangesAsync();
            return cantidad > 0;
            /* var trabajos = await _context.Trabajos
                 .Where(t => t.TrabajoId == trabajoId)
                 .ExecuteDeleteAsync();
             return trabajos > 0;*/
        }

        public async Task<List<TrabajosDetalle>> ListarTrabajoDetalle(int trabajoId)
        {

            await using var _context = await DbFactory.CreateDbContextAsync();
            var detalle = await _context.trabajosDetalles
                .Where(d => d.TrabajoId == trabajoId)
                .ToListAsync();

            return detalle;
        }

    }
}
