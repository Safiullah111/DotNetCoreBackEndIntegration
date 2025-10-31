using API.Work.Application.Contract.Common;
using System.ComponentModel.DataAnnotations;

namespace API.Work.Application.Contract.Services.Roles;

public class CreateRoleDto : EntityDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    public string Description { get; set; }
}
