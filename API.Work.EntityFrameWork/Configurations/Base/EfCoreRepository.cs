using API.Work.Domain.Configurations.GenericRepository;
using API.Work.EntityFrameWork.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace API.Work.EntityFrameWork.Configurations.Base;

public abstract class EfCoreRepository<TDbContext, TEntity, TKey> : IRepositoryBase<TEntity>
where TEntity : class
where TDbContext : DbContext
{
    protected readonly IDbContextProvider<TDbContext> _dbContextProvider;
    private readonly TDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    protected EfCoreRepository(IDbContextProvider<TDbContext> dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
        _dbContext = _dbContextProvider.GetDbContext();  // <-- get actual DbContext instance
        _dbSet = _dbContext.Set<TEntity>();              // <-- call Set<TEntity>() on DbContext
    }
    public virtual Task<DbSet<TEntity>> GetDbSetAsync()
    {
        return Task.FromResult(_dbSet);
    }

    public virtual Task<TDbContext> GetDbContextAsync()
    {
        return Task.FromResult(_dbContext);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

    public async Task<List<TEntity>> GetListAsync()
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.ToListAsync();
        }
        catch (DbUpdateException ex)
        {

            throw new GetByIdAsyncException("Get List", ex);
        }
    }

    public async Task<IQueryable<TEntity>> GetAll()
    {
        return await GetDbSetAsync(); // No ToListAsync here, just returning the query
    }

    public async Task<Guid> AddAsync(TEntity entity)
    {
        try
        {
            var addResult = await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return (Guid)addResult.Property("Id").CurrentValue!;
        }
        catch (DbUpdateException ex)
        {

            throw new AddAsyncException(entity.ToString(), ex);
        }
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            var affected = await _dbContext.SaveChangesAsync();
            return affected > 0;
        }
        catch (DbUpdateException ex)
        {
            //_logger.LogError(ex, "Database update error for {Entity}", typeof(TEntity).Name);

            throw new UpdateAsyncException(entity.ToString(), ex);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            var affected = await _dbContext.SaveChangesAsync();
            return affected > 0;
        }
        catch (DbUpdateException ex)
        {
            throw new DeleteAsyncException(id.ToString(), ex);
        }
    }
}
