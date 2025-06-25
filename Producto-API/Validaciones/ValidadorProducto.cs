using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Producto_API.DTOs;
using Producto_API.Entidades;

namespace Producto_API.Validaciones
{
    public class ValidadorProducto:AbstractValidator<CrearProductoDTO>
    {
        private readonly ApplicationDbContext context;

        public ValidadorProducto(ApplicationDbContext context)
        {
            this.context = context;

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .Length(2, 100).WithMessage("El nombre del producto debe tener entre 2 y 100 caracteres.")
                .MustAsync(NoExisteNombre).WithMessage("Ya existe un producto con ese nombre.");

            RuleFor(p => p.Descripcion)
                .NotEmpty().WithMessage("La descripción del producto es obligatoria.")
                .Length(5, 500).WithMessage("La descripción del producto debe tener entre 5 y 500 caracteres.");

            RuleFor(p => p.Precio)
                .NotEmpty().WithMessage("El precio no puede estar vacio")
                .GreaterThan(0).WithMessage("El precio del producto debe ser mayor que cero.");
           
        }

        private async Task<bool> NoExisteNombre(string nombre, CancellationToken cancellationToken)
        {
            return !await context.Productos.AnyAsync(p => p.Nombre == nombre, cancellationToken);
        }
    }
}
