using Producto_API.DTOs;
using Producto_API.Metodos;

namespace Producto_API.Servicios
{
    public interface IProductoService
    {
        Task<ResultadoServicio<ProductoDTO>> CrearProducto(CrearProductoDTO crearProductoDTO);
        Task<ResultadoServicio<ProductoDTO>> ObtenerProductoPorId(int id);
        Task<ResultadoServicio<List<ProductoDTO>>> ObtenerTodosLosProductos();
    }
}