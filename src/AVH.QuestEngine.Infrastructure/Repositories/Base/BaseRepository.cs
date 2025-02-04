using AVH.QuestEngine.Domain.Entities.Base;
using AVH.QuestEngine.Domain.Repositories.Base;
using AVH.QuestEngine.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AVH.QuestEngine.Infrastructure.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected QuestEngineDbContext _dbContext;
        protected DbSet<TEntity> _currentEntity;
        public BaseRepository(QuestEngineDbContext dbContext)
        {
            _dbContext = dbContext;
            _currentEntity = _dbContext.Set<TEntity>();

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _currentEntity.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _currentEntity.FindAsync(id);
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _currentEntity.AsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task AddAsync(TEntity entity)
        {
            _currentEntity.Add(entity);
            await SaveChangeAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _currentEntity.Remove(entity);
            await SaveChangeAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _currentEntity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await SaveChangeAsync();
        }

        protected async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
