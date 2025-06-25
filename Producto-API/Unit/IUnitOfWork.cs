using Producto_API.Entidades;
using Producto_API.Repositorio;

namespace Producto_API.Unit
{
    public interface IUnitOfWork
    {
        IGenericRepository<Producto> Producto { get; }

        Task<int> SaveChangesAsync();
    }
}