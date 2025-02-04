using AVH.QuestEngine.Domain.Entities.Base;

namespace AVH.QuestEngine.Domain.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {    
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> IsExistAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
