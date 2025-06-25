using System.ComponentModel.DataAnnotations;

namespace Producto_API.DTOs
{
    public class CrearProductoDTO
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }
    }
}
