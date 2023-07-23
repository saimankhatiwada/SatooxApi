namespace BussinessLogic.IRepositories;

public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    IQueryable<TEntity> GetQueryable();

    Task<TEntity?> GetByIdAsync(Guid id);

    Task InsertAsync(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    Task SaveChangesAsync();  
}

