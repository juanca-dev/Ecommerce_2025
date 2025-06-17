using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Entities
{
    public class Comentario
    {
        public int Id { get; set; }

        public Producto? Producto { get; set; }
        public int ProductoId { get; set; }
        public Usuario? Usuario { get; set; }
        public int UsuarioId { get; set; }
        public string Texto { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;


    }
}
