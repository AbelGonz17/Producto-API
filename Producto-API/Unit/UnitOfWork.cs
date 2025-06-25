using Producto_API.Entidades;
using Producto_API.Repositorio;

namespace Producto_API.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IGenericRepository<Producto> Producto { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Producto = new GenericRepository<Producto>(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

    }
}