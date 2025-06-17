using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Entities
{
    public class Carrito
     {
        public int Id { get; set; }
        public Usuario? Usuario { get; set; }
        public int UsuarioId { get; set; }
        public Producto? Producto { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public string? Comentario { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Total => Producto == null ? 0 : (decimal)Cantidad * Producto.Precio;
    }
}
