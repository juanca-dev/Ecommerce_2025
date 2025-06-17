using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Entities
{
    public class Valoracion
    {
        public int Id { get; set; }

        public Producto? Producto { get; set; }
        public int ProductoId { get; set; }
        public Usuario? Usuario { get; set; }
        public int UsuarioId { get; set; }
        public int Puntuacion { get; set; }

    }
}
