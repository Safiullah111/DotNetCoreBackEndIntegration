using API.Work.Application.Contract.Common;

namespace API.Work.Application.Contract.Services.Permissions;

public class GetPermissionListDto : PaginationDto
{
    public string Filter { get; set; }
}
