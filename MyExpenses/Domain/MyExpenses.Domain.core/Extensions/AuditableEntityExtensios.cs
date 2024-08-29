using MyExpenses.Domain.core.Entities.Base;
using MyExpenses.Domain.core.Entities.Enums;

namespace MyExpenses.Domain.core.Extensions
{
    public static class AuditableEntityExtensions
    {
        public static void UpdateAuditFieldsOnCreate(this AuditableEntity entity, Guid userId)
        {
            entity.CreatedOn = DateTime.UtcNow;
            entity.CreatedBy = userId;
            entity.ActivityStatusChangedOn = DateTime.UtcNow;
            entity.ActivityStatusChangedBy = userId;
            entity.ActivityStatus = ActivityStatus.Active; // Assuming `ActivityStatus.Created` is a valid enum value
        }

        public static void UpdateAuditFieldsOnUpdate(this AuditableEntity entity, Guid userId)
        {
            entity.UpdatedOn = DateTime.UtcNow;
            entity.UpdatedBy = userId;
            entity.ActivityStatusChangedOn = DateTime.UtcNow;
            entity.ActivityStatusChangedBy = userId;
            entity.ActivityStatus = ActivityStatus.Active; // Assuming `ActivityStatus.Updated` is a valid enum value
        }
    }

}
