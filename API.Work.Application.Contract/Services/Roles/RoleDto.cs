using API.Work.Application.Contract.Common;
using System.ComponentModel.DataAnnotations;

namespace API.Work.Application.Contract.Services.Roles;

public class RoleDto : EntityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
