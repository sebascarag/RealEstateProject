namespace RealEstate.Application.Contracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllActive();
        T Add(T entity);
        void Update(T entity);
        void Deactivate(T entity);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> SaveAsync();
    }
}
