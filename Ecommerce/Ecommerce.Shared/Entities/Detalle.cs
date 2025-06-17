﻿using Ecommerce.Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.Shared.Entities
{
    public class Detalle
    {
        public int Id { get; set; }

        [JsonIgnore]
        public Venta? Venta { get; set; }

        public int VentaId { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string? Comentario { get; set; }

        public Producto? Producto { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Total => Producto == null ? 0 : (decimal)Cantidad * Producto.Precio;
    }
}