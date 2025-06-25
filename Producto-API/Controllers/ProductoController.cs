using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Producto_API.DTOs;
using Producto_API.Entidades;
using Producto_API.Servicios;

namespace Producto_API.Controllers
{
    /// <summary>
    /// Controlador para operaciones sobre productos
    /// </summary>
    [ApiController]
    [Route("api/Producto")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService productoService;
        private readonly IValidator<CrearProductoDTO> validator;

        public ProductoController(IProductoService productoService,IValidator<CrearProductoDTO> validator)
        {
            this.productoService = productoService;
            this.validator = validator;
        }
        /// <summary>
        /// Obtiene todos los productos disponibles.
        /// </summary>
        /// <returns>Lista de productos</returns>
        [HttpGet("ObtenerTodosLosProductos")]
        [ProducesResponseType(typeof(List<ProductoDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ObtenerTodosLosProductos()
        {
            var resultado = await productoService.ObtenerTodosLosProductos();
            if (!resultado.Exitoso)
            {
                return NotFound(resultado.Mensaje);
            }
            return Ok(resultado.Datos);
        }

        /// <summary>
        /// Obtiene un producto por su identificador único.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>El producto encontrado</returns>
        /// <response code="200">Producto encontrado</response>
        /// <response code="404">Producto no encontrado</response>
        [HttpGet("ObtenerProductoPorId/{id:int}")]
        [ProducesResponseType(typeof(ProductoDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ObtenerProductoPorId(int id)
        {
            var resultado = await productoService.ObtenerProductoPorId(id);
            if (!resultado.Exitoso)
            {
                return NotFound(resultado);
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Crea un nuevo producto con los datos proporcionados.
        /// </summary>
        /// <param name="crearProductoDTO">Datos del producto a crear</param>
        /// <returns>Producto creado</returns>
        /// <response code = "201">Producto creado</response>
        /// <response code = "400">Dato invalidos</response>
        [HttpPost("CrearProducto")]
        [ProducesResponseType(typeof(ProductoDTO), 201)]
        [ProducesResponseType(typeof(List<ValidationFailure>), 400)]


        public async Task<ActionResult<ProductoDTO>> CrearProducto([FromBody] CrearProductoDTO crearProductoDTO)
        {
            var resultadovalidaciones = await validator.ValidateAsync(crearProductoDTO);

            if (!resultadovalidaciones.IsValid)
            {
                var erroresSimplificados = resultadovalidaciones.Errors.Select(e => new
                {
                    Campo = e.PropertyName,
                    Mensaje = e.ErrorMessage
                });

                return BadRequest(erroresSimplificados);
            }

            var resultado = await productoService.CrearProducto(crearProductoDTO);
            return CreatedAtAction(nameof(ObtenerProductoPorId), new { id = resultado.Datos.Id }, resultado.Datos);
        }
    }
}
