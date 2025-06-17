using Ecommerce.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Models
{
    public class CarritoDTO
    {
        public Usuario? Usuario { get; set; }

        public int UsuarioId { get; set; }

        public string? Comentario { get; set; }

        public ICollection<Carrito>? Items { get; set; }

        public int Cantidad => Items == null ? 0 : Items.Sum(ts => ts.Cantidad);

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Total => Items == null ? 0 : Items.Sum(ts => ts.Total);
    }
}