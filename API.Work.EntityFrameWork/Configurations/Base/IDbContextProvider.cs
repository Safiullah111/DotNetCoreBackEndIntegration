using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Work.EntityFrameWork.Configurations.Base
{
    public interface IDbContextProvider<TDbContext>
       where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}
