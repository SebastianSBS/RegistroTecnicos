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
                var articulo = await _context.Articulos.FindAsync(detalle.ArticuloId);

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


        public async Task<bool> Modificar(Trabajos trabajo)
        {
            var trabajoAnterior = await _context.Trabajos
                .Include(t => t.trabajosDetalle)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TrabajoId == trabajo.TrabajoId);

            if (trabajoAnterior != null)
            {
              
                foreach (var detalle in trabajoAnterior.trabajosDetalle)
                {
                    var articulo = await _context.Articulos.FindAsync(detalle.ArticuloId);
                    if (articulo != null)
                    {
                        articulo.Existencia += detalle.Cantidad;
                        _context.Articulos.Update(articulo);
                    }
                }

                _context.trabajosDetalles.RemoveRange(trabajoAnterior.trabajosDetalle);
                await _context.SaveChangesAsync();
            }

            foreach (var detalle in trabajo.trabajosDetalle)
            {
                var articulo = await _context.Articulos.FindAsync(detalle.ArticuloId);
                if (articulo != null)
                {
                    articulo.Existencia -= detalle.Cantidad;
                    _context.Articulos.Update(articulo);
                }
            }

            _context.Trabajos.Update(trabajo);
            await _context.SaveChangesAsync();
            return true;
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
                foreach (var detalle in trabajo.trabajosDetalle)
                {
                    var articulo = await BuscarArticulos(detalle.ArticuloId);
                    if (articulo != null)
                    {
                        articulo.Existencia -= detalle.Cantidad;
                        await ActualizarArticulo(articulo);
                    }
                }
                return await Insertar(trabajo);
            }
            else
                return await Modificar(trabajo);
        }

        public async Task<List<TrabajosDetalle>> BuscarDetalle(int id)
        {
            return await _context.trabajosDetalles
                //.Include(td => td.Articulo)
                .Where(td => td.TrabajoId == id)
                 .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Eliminar(int id)
        {
            var detalles = await BuscarDetalle(id);

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
                .Where(t => t.TrabajoId == id)
                .ExecuteDeleteAsync();
            return trabajos > 0;
        }

    }
}
