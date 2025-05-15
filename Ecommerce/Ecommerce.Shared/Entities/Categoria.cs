using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Entities
{
    // dejarla en public para tener acceso
    public class Categoria
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        //lo inicializamos ya que esa propiedad acepta nulos. (con un string vacio )
        public string Nombre { get; set; } = string.Empty;
        // aqui los pongo ??A
       


    }
}
