using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Work.Domain.Services
{
    public interface IUserDomainService
    {
        bool IsEmailUnique(string email);
    }

}
