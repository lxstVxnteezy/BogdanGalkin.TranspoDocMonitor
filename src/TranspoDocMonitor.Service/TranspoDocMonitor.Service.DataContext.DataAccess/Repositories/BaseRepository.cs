using Microsoft.EntityFrameworkCore;
using TranspoDocMonitor.Service.Domain.Base;

namespace TranspoDocMonitor.Service.DataContext.DataAccess.Repositories
{
    public class BaseRepository <TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly ServiceContext _context;

        public BaseRepository(ServiceContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Query => _context.Set<TEntity>();

        public TEntity GetById(Guid id)
        {
            return _context.Set<TEntity>().Single(x => x.Id == id);
        }

        public TEntity? FoundById(Guid id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public Task<TEntity?> FoundByIdAsync(Guid id, CancellationToken ctn)
        {
            return _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id, ctn);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(TEntity[] entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void RemoveById(Guid id)
        {
            var entity = GetById(id);
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveByIds(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges(CancellationToken ctn)
        {
            return _context.SaveChangesAsync(ctn);
        }
    }
}
