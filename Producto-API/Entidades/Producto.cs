using Producto_API.Repositorio;

namespace Producto_API.Entidades
{
    /// <summary>
    /// Representa los datos de un producto.
    /// </summary>
    public class Producto:IEntidadBase
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Descripcio del producto.
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Precio del producto.
        /// </summary>
        public decimal Precio { get; set; }
    }
}
