using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TrabajoServices
    {
        private readonly Context _context;

        public TrabajoServices(Context context)
        {
            _context = context;
        }

        public async Task<bool> Existe(int trabajoId)
        {
            return await _context.Trabajos.AnyAsync(t => t.TrabajoId == trabajoId);
        }

        public async Task<bool> Insertar(Trabajos trabajo)
        {
            
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
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> Modificar(Trabajos trabajo)
        {
            _context.Update(trabajo);
            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<Trabajos?> Buscar(int trabajoId)
        {
            return await _context.Trabajos
                .Include(t => t.trabajosDetalle)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TrabajoId == trabajoId);
        }

        public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
        {
            return await _context.Trabajos
                .Include(t => t.trabajosDetalle)
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<List<Articulos>> GetArticulos()
        {
            return await _context.Articulos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Articulos> BuscarArticulos(int id)
        {
            return await _context.Articulos
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ArticuloId == id);
        }

        public async Task<bool> ActualizarArticulo(Articulos articulo)
        {
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
            return await _context.trabajosDetalles
                .Include(a => a.Articulos)
                .Where(t => t.TrabajoId == trabajoId)
                 .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Eliminar(int trabajoId)
        {
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

            var trabajos = await _context.Trabajos
                .Where(t => t.TrabajoId == trabajoId)
                .ExecuteDeleteAsync();
            return trabajos > 0;
        }

        public async Task<List<TrabajosDetalle>> ListarTrabajoDetalle(int trabajoId)
        {
            var detalle = await _context.trabajosDetalles
                .Where(d => d.TrabajoId == trabajoId)
                .ToListAsync();

            return detalle;
        }

    }
}
