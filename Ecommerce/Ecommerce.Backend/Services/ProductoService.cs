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

        public async Task<ActionResponse<Comentario>> AddComentarioAsync(Comentario comentario)
        {
            var producto = await _repository.GetByIdAsync(comentario.ProductoId);
            if (producto == null)
            {
                return new ActionResponse<Comentario> { Success = false, Message = "El producto no existe." };
            }
            var nuevoComentario = new Comentario
            {
                ProductoId = comentario.ProductoId,
                UsuarioId = comentario.UsuarioId,
                Texto = comentario.Texto
            };
            await _repository.AddCommentAsync(nuevoComentario);
            return new ActionResponse<Comentario> { Success = true, Result = nuevoComentario };
        }

        public async Task<ActionResponse<Valoracion>> AddRatingAsync(Valoracion valoracion)
        {
            var producto = await _repository.GetByIdAsync(valoracion.ProductoId);
            if (producto == null)
            {
                return new ActionResponse<Valoracion> { Success = false, Message = "El producto no existe." };
            }

            if (valoracion.Puntuacion < 1 || valoracion.Puntuacion > 5)
            {
                return new ActionResponse<Valoracion> { Success = false, Message = "La calificación debe estar entre 1 y 5." };
            }

            var nuevaValoracion = new Valoracion
            {
                ProductoId = valoracion.ProductoId,
                UsuarioId = valoracion.UsuarioId,
                Puntuacion = valoracion.Puntuacion
            };

            await _repository.AddStarsAsync(nuevaValoracion);
            var puntuacion = await GetRatingAsync(nuevaValoracion.ProductoId);
            await _repository.UpdateRating(nuevaValoracion.ProductoId, puntuacion);
            return new ActionResponse<Valoracion> { Success = true, Result = nuevaValoracion };
        }

        public async Task<IEnumerable<Comentario>> GetComentariosAsync(int productoId)
        {
            return await _repository.GetComentariosAsync(productoId);
        }

        public async Task<double> GetRatingAsync(int productoId)
        {
            var valoraciones = await _repository.GetValoracionesAsync(productoId);
            var promedio = valoraciones.Average(v => v.Puntuacion);
            return promedio;
        }
    }
}