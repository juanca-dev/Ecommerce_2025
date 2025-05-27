using Ecommerce.Backend.Repositories;
using Ecommerce.Shared.Entities;
using Ecommerce.Shared.Responses;

namespace Ecommerce.Backend.Services
{
    public class CategoriaService(ICategoriaRepository repository)
    {
        private readonly ICategoriaRepository _repository = repository;

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ActionResponse<Categoria>> AddAsync(Categoria categoria)
        {
            // validamos si ya existe una categoria con ese nombre
            var existe = await _repository.ExisteNombreAsync(categoria.Nombre);
            //si existe, retornamos un error
            if (existe)
            {
                return new ActionResponse<Categoria> { Success = false, Message = "Ya existe una categoría con ese nombre." };
            }
            //si no existe enviamos la categoria al repository para que la guarde en la base de datos
            var nuevaCategoria = new Categoria { Nombre = categoria.Nombre };
            await _repository.AddAsync(nuevaCategoria);

            //retornarnamos la respuesta al controlador
            return new ActionResponse<Categoria> { Success = true, Result = nuevaCategoria };
        }

        public async Task<ActionResponse<Categoria>> UpdateAsync(Categoria categoria)
        {
            //validamos que exista la categoria
            var categoriaExistente = await _repository.GetByIdAsync(categoria.Id);
            if (categoriaExistente == null)
            {
                return new ActionResponse<Categoria>
                {
                    Success = false,
                    Message = "La categoría no existe."
                };
            }

            //validamos si ya existe una categoria con ese nombre
            var existe = await _repository.ExisteNombreAsync(categoria.Nombre);

            if (existe)
            {
                return new ActionResponse<Categoria>
                {
                    Success = false,
                    Message = "Ya existe otra categoría con ese nombre."
                };
            }

            //asignamos el nuevo nombre a la categoria
            categoriaExistente.Nombre = categoria.Nombre;
            await _repository.UpdateAsync(categoriaExistente);

            return new ActionResponse<Categoria>
            {
                Success = true,
                Result = categoriaExistente
            };
        }
    }
}