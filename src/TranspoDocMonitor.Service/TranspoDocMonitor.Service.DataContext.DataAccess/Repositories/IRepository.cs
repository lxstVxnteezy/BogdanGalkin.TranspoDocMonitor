

namespace TranspoDocMonitor.Service.DataContext.DataAccess.Repositories
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Query { get; }

        TEntity GetById(Guid id);

        TEntity? FoundById(Guid id);

        Task<TEntity?> FoundByIdAsync(Guid id, CancellationToken ctn);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(TEntity[] entities);

        void RemoveById(Guid id);

        void RemoveByIds(IEnumerable<Guid> ids);

        Task SaveChanges(CancellationToken ctn = default);
    }
}
