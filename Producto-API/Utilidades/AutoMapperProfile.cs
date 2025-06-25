using AutoMapper;
using Producto_API.DTOs;
using Producto_API.Entidades;

namespace Producto_API.Utilidades
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Producto, ProductoDTO>();
            CreateMap<CrearProductoDTO, Producto>();               
        }
    }
}
