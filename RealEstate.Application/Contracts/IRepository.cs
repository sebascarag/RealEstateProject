using System.Linq.Expressions;

namespace RealEstate.Application.Contracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllActive();
        IQueryable<T> GetAllActiveIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<IList<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken);
        T Add(T entity);
        void Update(T entity);
        void Deactivate(T entity);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> SaveAsync(CancellationToken cancellationToken);
    }
}
