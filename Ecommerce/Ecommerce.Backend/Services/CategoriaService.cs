using Ecommerce.Backend.Repositories;
using Ecommerce.Shared.Entities;
using Ecommerce.Shared.Responses;

namespace Ecommerce.Backend.Services
{
    // Esta capa se va 
    public class CategoriaService(ICategoriaRepository repository)
    {
        // Aqui estamos inyectando el repositorio ya podemos acceder a la capa Datos.
        // Agregamos metodos
        private readonly ICategoriaRepository _repository = repository;

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            // se devuelve la lista de categorias.
            // Aun no hay regla de negocio
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

            // Aqui se valida si ya se existe la categoria con ese nombre.

            var existe = await _repository.ExisteNombreAsync(categoria.Nombre);
            // si existe, retornamos un error
            if (existe)
            {
                return new ActionResponse<Categoria> { Success = false, Message = "Ya existe una categoria con ese nombre." };
            }
            // si no existe enviamos la categoria al reposoitory para que la guarde en la base de datos

            var nuevaCategoria = new Categoria { Nombre = categoria.Nombre };

            await _repository.AddAsync(nuevaCategoria);
            // retornamos la respuesta al controlador
            return new ActionResponse<Categoria> { Success = true, Result = nuevaCategoria };

        }

        public async Task<ActionResponse<Categoria>> UpdateAsync(Categoria categoria)
        {
            // Validamos que existe la categoría
            var categoriaExistente = await _repository.GetByIdAsync(categoria.Id);
            if (categoriaExistente == null)
            {
                return new ActionResponse<Categoria>
                {
                    Success = false,
                    Message = "La categoría no existe."
                };
            }

            // Validamos si ya existe otra categoría con el mismo nombre
            var existe = await _repository.ExisteNombreAsync(categoria.Nombre);
            if (existe)
            {
                return new ActionResponse<Categoria>
                {
                    Success = false,
                    Message = "Ya existe una categoría con ese nombre."
                };
            }

            // Actualizamos el nombre
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
