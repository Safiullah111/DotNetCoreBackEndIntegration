using API.Work.Domain.Configurations.GenericRepository;
using AutoMapper;

namespace API.Work.Application.Configurations.GenericServices;

public abstract class AppServiceBase<TEntity> : IAppServiceBase<TEntity> where TEntity : class
{
    protected readonly IRepositoryBase<TEntity> _repository;
    
    public AppServiceBase(IRepositoryBase<TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetListAsync()
    {
        return await _repository.GetListAsync();
    }

    public virtual async Task<Guid> CreateAsync(TEntity dto)
    {
        return await _repository.AddAsync(dto);
    }

    public virtual async Task UpdateAsync(TEntity dto)
    {
        await _repository.UpdateAsync(dto);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}


