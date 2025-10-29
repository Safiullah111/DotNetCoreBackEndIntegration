using API.Work.Application.Contract.Common;

namespace API.Work.Application.Contract.Services.Roles;

public class GetRoleListDto : PaginationDto
{
    public string Filter { get; set; }
}
