using AutoMapper;
using FluentAssertions;
using Moq;
using Producto_API.DTOs;
using Producto_API.Entidades;
using Producto_API.Repositorio;
using Producto_API.Servicios;
using Producto_API.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto_API.Test.Tests
{
    public class ProductoServiceTests
    {
        [Fact]
        public async Task CrearProducto_GuardadoCorrectamente()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var MockProductoRepo = new Mock<IGenericRepository<Producto>>();

            mockUnitOfWork.Setup(u => u.Producto).Returns(MockProductoRepo.Object);

            var servicio = new ProductoService(
                mockUnitOfWork.Object,
                mockMapper.Object
            );

            var crearProductoDTO = new CrearProductoDTO
            {
                Nombre = "Producto Test",
                Descripcion = "Descripcion del producto test",
                Precio = 100.0m
            };

            var producto = new Producto
            {
                Id = 1,
                Nombre = crearProductoDTO.Nombre,
                Descripcion = crearProductoDTO.Descripcion,
                Precio = crearProductoDTO.Precio
            };

            var productoDto = new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio
            };

            mockMapper.Setup(m => m.Map<Producto>(crearProductoDTO)).Returns(producto);
            mockMapper.Setup(m => m.Map<ProductoDTO>(producto)).Returns(productoDto);

            MockProductoRepo.Setup(repo => repo.AddAsync(producto)).Returns(Task.CompletedTask);
            mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            //Act
            var resultado = await servicio.CrearProducto(crearProductoDTO);

            //Assert
            resultado.Exitoso.Should().BeTrue();
            resultado.Datos.Should().BeEquivalentTo(productoDto);

            MockProductoRepo.Verify(repo => repo.AddAsync(It.IsAny<Producto>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
