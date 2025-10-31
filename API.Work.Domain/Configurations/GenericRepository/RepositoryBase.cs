using API.Work.Domain.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Work.Domain.Configurations.GenericRepository;


public  class RepositoryBase<TEntity,TContext> : IRepositoryBase<TEntity> where TEntity : class where TContext : DbContext
{
    protected readonly TContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

    public async Task<List<TEntity>> GetListAsync() => await _dbSet.ToListAsync();
    public async Task<IQueryable<TEntity>> GetAll() => await Task.FromResult(_dbSet.AsQueryable());

    public async Task<Guid> AddAsync(TEntity entity)
    {
        var addEntity = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return addEntity.IsKeySet ? (Guid)addEntity.Property("Id").CurrentValue! : Guid.Empty;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        int affectedRow = await _context.SaveChangesAsync();
        return affectedRow > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            int affectedRow = await _context.SaveChangesAsync();

            return affectedRow > 0;
        }

        return false; ;
    }
}

