using MyExpenses.Domain.core.Entities.Enums;

namespace MyExpenses.Domain.core.Entities.Base
{
    public interface IAuditableEntity
    {
            public DateTime CreatedOn { get; set; }
            public DateTime? UpdatedOn { get; set; }
            public Guid CreatedBy { get; set; }
            public Guid? UpdatedBy { get; set; }
            public DateTime? ActivityStatusChangedOn { get; set; }
            public Guid? ActivityStatusChangedBy { get; set; }
            public ActivityStatus ActivityStatus { get; set; }
    }
}
