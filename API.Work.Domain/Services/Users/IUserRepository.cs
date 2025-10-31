using API.Work.Domain.Configurations.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Work.Domain.Services.Users
{
   public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> FindByNameAsync(string name);
        Task<User?> GetUserAsync(string email);

        Task<List<User>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
