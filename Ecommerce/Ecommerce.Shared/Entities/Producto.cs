using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Shared.Entities
{
    public class Producto
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0.01, 99999.99, ErrorMessage = "El precio debe estar entre 0.01 y 99999.99")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayName("Unidades disponibles")]
        public int Stock { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string? Descripcion { get; set; }

        [Display(Name = "Foto")]
        public string? URLFoto { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public double Calificacion { get; set; } = 5;
    }
}