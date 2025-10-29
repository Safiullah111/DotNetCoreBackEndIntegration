using API.Work.Domain.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Work.Domain.Shared.Common.BaseEntity;

public abstract class BaseEntity : IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset DateDeleted { get; set; }
    public Guid CreaatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public BaseEntity(Guid id) => Id = id;
    public BaseEntity()
    {
    }
}