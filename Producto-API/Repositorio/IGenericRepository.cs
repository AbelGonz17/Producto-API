using System.Linq.Expressions;

namespace Producto_API.Repositorio
{
    public interface IGenericRepository<T> where T : class, IEntidadBase
    {
        Task AddAsync(T entity);
        Task<int> ContarAsync(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
        Task<List<T>> GetAllAsync(string includeProperties = "");
        Task<T> GetByAsyncId(int id, string includeProperties = "");
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> condition, string includeProperties = "");
        void Update(T entity);
    }
}