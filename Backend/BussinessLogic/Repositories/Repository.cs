using BussinessLogic.IRepositories;
using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace BussinessLogic.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{

    public readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return _context.Set<TEntity>();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }
}
