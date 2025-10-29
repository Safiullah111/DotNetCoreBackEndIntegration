
namespace API.Work.Application.Configurations.GenericServices;

public interface IAppServiceBase<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetListAsync();
    Task<Guid> CreateAsync(TEntity dto);
    Task UpdateAsync(TEntity dto);
    Task DeleteAsync(Guid id);
}
