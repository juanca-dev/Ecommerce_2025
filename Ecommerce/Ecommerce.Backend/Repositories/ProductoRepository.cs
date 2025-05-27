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
    }
}