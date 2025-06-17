using Ecommerce.Backend.Data;
using Ecommerce.Shared.Entities;
using Ecommerce.Shared.Models;
using Ecommerce.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Backend.Repositories
{
    public class VentaRepository(DataContext context) : IVentaRepository
    {
        private readonly DataContext _context = context;

        public Task<ActionResponse<Producto>> ConfirmStock(CarritoDTO carrito)
        {
            throw new NotImplementedException();
        }

        //borrar un item del carrito
        public async Task DeleteCartAsync(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id) ?? throw new Exception($"Carrito con id {id} no encontrado");
            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();
        }

        //obtener un carrito por id
        public async Task<Carrito> GetCartAsync(int id)
        {
            var carrito = await _context.Carritos
                .Include(c => c.Producto)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception($"Carrito con id {id} no encontrado");
            return carrito;
        }

        //obtenr todos los carritos
        public async Task<IEnumerable<Carrito>> GetCartsAsync()
        {
            var carritos = await _context.Carritos
                .Include(c => c.Producto)
                .Include(c => c.Usuario)
                .ToListAsync();

            return carritos;
        }

        public Task<IEnumerable<Detalle>> GetDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Venta> GetVentaAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Venta>> GetVentasAsync()
        {
            throw new NotImplementedException();
        }

        //guardar un carrito
        public async Task SaveCartAsync(Carrito carrito)
        {
            await _context.AddAsync(carrito);
            await _context.SaveChangesAsync();
        }

        public Task SaveOrderAsync(Venta venta)
        {
            throw new NotImplementedException();
        }

        //actualizar carrito
        public async Task UpdateCartAsync(Carrito carrito)
        {
            _context.Carritos.Update(carrito);
            await _context.SaveChangesAsync();
        }

        public Task UpdateStatusAsync(Venta venta)
        {
            throw new NotImplementedException();
        }
    }
}