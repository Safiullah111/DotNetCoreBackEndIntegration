
using API.Work.Domain.Shared.Interfaces;

namespace API.Work.Application.Contract.Common
{
    public class EntityDto : IEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? DateUpdated { get; set; }
        public DateTimeOffset DateDeleted { get; set; }
        public Guid CreaatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
