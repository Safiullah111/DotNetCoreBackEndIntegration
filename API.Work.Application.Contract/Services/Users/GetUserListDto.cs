using API.Work.Application.Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Work.Application.Contract.Services.Users;

public class GetUserListDto: PaginationDto
{
    public string Filter { get; set; }
}
