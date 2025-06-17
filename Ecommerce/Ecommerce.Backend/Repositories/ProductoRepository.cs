using Ecommerce.Backend.Data;
using Ecommerce.Backend.Services;
using Ecommerce.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Backend.Repositories
{
    public class ProductoRepository(DataContext context, IFilesService filesService) : IProductoRepository
    {
        private readonly DataContext _context = context;
        private readonly IFilesService _service = filesService;

        public async Task AddAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id) ?? throw new Exception($"Producto con id {id} no fue encontrado.");
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            var productos = await _context.Productos
               .Include(p => p.Categoria)
               .ToListAsync();

            return productos;
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            var producto = await _context.Productos
                 .Include(p => p.Categoria)
                 .FirstOrDefaultAsync(p => p.Id == id) ?? throw new Exception($"Producto con id {id} no fue encontrado.");

            return producto;
        }

        public async Task UpdateAsync(Producto producto)
        {
            var productoExistente = await _context.Productos.FindAsync(producto.Id) ?? throw new Exception($"Producto con id {producto.Id} no fue encontrado.");
            productoExistente.Nombre = producto.Nombre;
            productoExistente.Precio = producto.Precio;
            productoExistente.Stock = producto.Stock;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Categoria = producto.Categoria;

            if (!string.IsNullOrEmpty(producto.URLFoto) && producto.URLFoto != productoExistente.URLFoto)
            {
                productoExistente.URLFoto = producto.URLFoto;
            }
            else if (string.IsNullOrEmpty(productoExistente.URLFoto) && !string.IsNullOrEmpty(producto.URLFoto))
            {
                productoExistente.URLFoto = await _service.UploadImage(producto.URLFoto);
            }

            _context.Entry(productoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception($"Categoria con id {id} no fue encontrada.");
            return categoria;
        }

        public async Task<(IEnumerable<Producto> Productos, int TotalCount)> GetPaginatedAsync(int page, int pageSize)
        {
            var totalCount = await _context.Productos.CountAsync();
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (productos, totalCount);
        }

        public async Task AddCommentAsync(Comentario comentario)
        {
            await _context.Comentarios.AddAsync(comentario);
            await _context.SaveChangesAsync();
        }

        public async Task AddStarsAsync(Valoracion valoracion)
        {
            await _context.Valoraciones.AddAsync(valoracion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comentario>> GetComentariosAsync(int productoId)
        {
            var comentarios = await _context.Comentarios
                .Include(c => c.Usuario)
                .Where(c => c.ProductoId == productoId)
                .ToListAsync();
            return comentarios;
        }

        public async Task<IEnumerable<Valoracion>> GetValoracionesAsync(int productoId)
        {
            var valoraciones = await _context.Valoraciones
                .Include(v => v.Usuario)
                .Where(v => v.ProductoId == productoId)
                .ToListAsync();
            return valoraciones;
        }

        public async Task UpdateRating(int productoId, double puntuacion)
        {
            var producto = await _context.Productos.FindAsync(productoId) ?? throw new Exception($"Producto con id {productoId} no fue encontrado.");
            producto.Calificacion = puntuacion;
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}