namespace MarketNet.src.Infraestructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(long id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<long> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}
