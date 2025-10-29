using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using API.Work.EntityFrameWork.Configurations.Base;
using API.Work.EntityFrameWork.Configurations;
using API.Work.Domain.Services.Users;

namespace API.Work.EntityFrameWork.Repositories.Users;

public class EfCoreUserRepository : EfCoreRepository<APIWorkDbContext, User, Guid>, IUserRepository
{
    public EfCoreUserRepository(IDbContextProvider<APIWorkDbContext> dbContext) : base(dbContext)
    {
    }

    public async Task<User?> FindByNameAsync(string name)
    {
        string userName = name.Trim().ToLower();
        return await (await GetDbSetAsync()).FirstOrDefaultAsync(u => u.FirstName.ToLower() == userName);
    }

    public async Task<User?> GetUserAsync(string email)
    {
        string userName = email.Trim().ToLower();

        return await (await GetDbSetAsync())
            .FirstOrDefaultAsync(u => u.UserName == userName || u.UserEmail == userName);
    }

    public async Task<List<User>> GetListAsync(int skipCount,int maxResultCount,string sorting,string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        var user = (await GetDbContextAsync());
        
        return await dbSet
            .WhereIf(
                !string.IsNullOrWhiteSpace(filter),
                author => author.FirstName.Contains(filter)
                )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}
