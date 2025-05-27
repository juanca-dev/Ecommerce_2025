using Ecommerce.Backend.Data;
using Ecommerce.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Backend.Repositories
{
    public class CategoriaRepository(DataContext context) : ICategoriaRepository
    {
        private readonly DataContext _context = context;

        public async Task AddAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id) ?? throw new Exception($"Categoria con id {id} no fue encontrada.");
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            var categorias = await _context.Categorias
                .ToListAsync();
            return categorias;
        }

        public async Task<Categoria> GetByIdAsync(int id)
        {
            var categoria = await _context.Categorias
               .FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception($"Categoria con id {id} no fue encontrada.");
            return categoria;
        }

        public async Task UpdateAsync(Categoria categoria)
        {
            var categoriaExistente = await _context.Categorias.FindAsync(categoria.Id) ?? throw new Exception($"Categoria con id {categoria.Id} no fue encontrada.");
            categoriaExistente.Nombre = categoria.Nombre;
            _context.Entry(categoriaExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _context.Categorias.AnyAsync(c => c.Nombre == nombre);
        }
    }
}