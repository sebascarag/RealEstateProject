using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RealEstate.Application.Contracts;
using System.Linq.Expressions;

namespace RealEstate.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private RealEstateDbContext DbContext { get; set; }
        private DbSet<T> DbSet { get; set; }

        public Repository(RealEstateDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        public IQueryable<T> GetAllActive()
        {
            var arg = Expression.Parameter(typeof(T), "p");
            var body = Expression.Call(Expression.Property(arg, "Active"), "Equals", null, Expression.Constant(true));
            var predicate = Expression.Lambda<Func<T, bool>>(body, arg);
            return DbSet.Where(predicate);
        }

        public IQueryable<T> GetAllActiveIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(GetAllActive(), (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task<IList<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken)
        {
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellation) => await DbSet.FindAsync(id);

        public T Add(T entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Add(entity);
            }
            dbEntityEntry.State = EntityState.Added;

            return dbEntityEntry.Entity;
        }

        public void AddRange(IEnumerable<T> entities) 
        { 
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public void Update(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Deactivate(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            dbEntityEntry.State = EntityState.Unchanged;
            SetValue(dbEntityEntry, "Active", false);
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken) => await DbContext.SaveChangesAsync(cancellationToken) > 0;

        private void SetValue(EntityEntry entity, string propertyName, object newValue)
        {
            entity.Entity.GetType().GetProperty(propertyName)?.SetValue(entity.Entity, newValue);
            entity.Property(propertyName).IsModified = true;
        }
    }
}
