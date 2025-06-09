using Ecommerce.Backend.Repositories;
using Ecommerce.Shared.Entities;
using Ecommerce.Shared.Responses;

namespace Ecommerce.Backend.Services
{
    public class ProductoService(IProductoRepository repository, IFilesService service)
    {
        private readonly IProductoRepository _repository = repository;
        private readonly IFilesService _service = service;

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ActionResponse<Producto>> AddAsync(Producto producto)
        {
            var categoria = await _repository.GetCategoriaByIdAsync(producto.Categoria!.Id);
            if (categoria == null)
            {
                return new ActionResponse<Producto> { Success = false, Message = "La categoría no existe." };
            }

            var nuevoProducto = new Producto
            {
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Descripcion = producto.Descripcion,
                CategoriaId = categoria.Id,
                URLFoto = await _service.UploadImage(producto.URLFoto!)
            };
            await _repository.AddAsync(nuevoProducto);
            return new ActionResponse<Producto> { Success = true, Result = nuevoProducto };
        }

        public async Task<ActionResponse<Producto>> UpdateAsync(Producto producto)
        {
            var productoExistente = await _repository.GetByIdAsync(producto.Id);
            if (productoExistente == null)
            {
                return new ActionResponse<Producto>
                {
                    Success = false,
                    Message = "El producto no existe."
                };
            }

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Precio = producto.Precio;
            productoExistente.Stock = producto.Stock;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Categoria = await _repository.GetCategoriaByIdAsync(producto.Categoria!.Id);

            if (!string.IsNullOrEmpty(producto.URLFoto) && producto.URLFoto != productoExistente.URLFoto)
            {
                productoExistente.URLFoto = producto.URLFoto;
            }
            else if (string.IsNullOrEmpty(productoExistente.URLFoto) && !string.IsNullOrEmpty(producto.URLFoto))
            {
                productoExistente.URLFoto = await _service.UploadImage(producto.URLFoto);
            }
            await _repository.UpdateAsync(productoExistente);
            return new ActionResponse<Producto>
            {
                Success = true,
                Result = productoExistente
            };
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<(IEnumerable<Producto> Productos, int TotalCount)> GetPaginatedAsync(int page, int pageSize)
        {
            return await _repository.GetPaginatedAsync(page, pageSize);
        }
    }
}