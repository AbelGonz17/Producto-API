using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Producto_API.DTOs;
using Producto_API.Entidades;
using Producto_API.Metodos;
using Producto_API.Unit;

namespace Producto_API.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ResultadoServicio<List<ProductoDTO>>> ObtenerTodosLosProductos()
        {
            var productos = await unitOfWork.Producto.GetAllAsync();
            if (productos == null || !productos.Any())
            {
                return ResultadoServicio<List<ProductoDTO>>.Fallo("No se encontraron productos.");
            }
            var productosDTO = mapper.Map<List<ProductoDTO>>(productos);
            return ResultadoServicio<List<ProductoDTO>>.Ok(productosDTO);
        }

        public async Task<ResultadoServicio<ProductoDTO>> ObtenerProductoPorId(int id)
        {
            var producto = await unitOfWork.Producto.GetByAsyncId(id);
            if (producto == null)
            {
                return ResultadoServicio<ProductoDTO>.Fallo("Producto no encontrado.");
            }
            var productoDTO = mapper.Map<ProductoDTO>(producto);
            return ResultadoServicio<ProductoDTO>.Ok(productoDTO);
        }

        public async Task<ResultadoServicio<ProductoDTO>> CrearProducto(CrearProductoDTO crearProductoDTO)
        {          
            var producto = mapper.Map<Producto>(crearProductoDTO);
            await unitOfWork.Producto.AddAsync(producto);
            await unitOfWork.SaveChangesAsync();
            var productoDTO = mapper.Map<ProductoDTO>(producto);
            return ResultadoServicio<ProductoDTO>.Ok(productoDTO);
        }
    }
}
