using Producto_API.Repositorio;
using Producto_API.Servicios;
using Producto_API.Unit;

namespace Producto_API.Utilidades
{
    public static class ServiceExtension
    {
        public static void  AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
