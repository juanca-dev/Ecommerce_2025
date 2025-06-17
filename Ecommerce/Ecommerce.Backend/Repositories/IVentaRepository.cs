using Ecommerce.Shared.Entities;
using Ecommerce.Shared.Models;
using Ecommerce.Shared.Responses;

namespace Ecommerce.Backend.Repositories
{
    public interface IVentaRepository
    {
        Task SaveCartAsync(Carrito carrito);

        Task<IEnumerable<Carrito>> GetCartsAsync();

        Task<IEnumerable<Venta>> GetVentasAsync();

        Task<IEnumerable<Detalle>> GetDetailsAsync();

        Task<Carrito> GetCartAsync(int id);

        Task<Venta> GetVentaAsync(int id);

        Task UpdateCartAsync(Carrito carrito);

        Task DeleteCartAsync(int id);

        Task SaveOrderAsync(Venta venta);

        Task<ActionResponse<Producto>> ConfirmStock(CarritoDTO carrito);

        Task UpdateStatusAsync(Venta venta);
    }
}