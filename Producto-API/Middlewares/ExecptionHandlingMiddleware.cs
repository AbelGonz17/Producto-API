using Microsoft.AspNetCore.Diagnostics;

namespace Producto_API.Middlewares
{
    public class ExecptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExecptionHandlingMiddleware> logger;
        private readonly IWebHostEnvironment env;

        public ExecptionHandlingMiddleware(RequestDelegate next, ILogger<ExecptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex) {
                logger.LogError(ex, "Error inesperado");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    Mensaje = "Ocurrio un error inesperado en el servidor.",
                    Detalles = env.IsDevelopment() ? ex.Message : null
                };

                await context.Response.WriteAsJsonAsync(response);
            }

        }
    }
}
