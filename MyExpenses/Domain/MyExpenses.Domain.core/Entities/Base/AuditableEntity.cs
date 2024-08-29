using MyExpenses.Domain.core.Entities.Enums;

namespace MyExpenses.Domain.core.Entities.Base
{
    public class AuditableEntity:BaseEntity
    {
        /// <summary>
        /// gets or sets the created date of the entity
        /// </summary>
        /// <value> type of DateTime</value>
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// gets or sets the updated date of the entity
        /// </summary>
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// gets or sets the created by of the entity
        /// </summary>
        public Guid CreatedBy { get; set; }
        /// <summary>
        /// gets or sets the updated by of the entity
        /// </summary>
        public Guid? UpdatedBy { get; set; }
        /// <summary>
        /// gets or sets the activity status changed on of the entity
        /// </summary>
        public DateTime? ActivityStatusChangedOn { get; set; }
        /// <summary>
        /// gets or sets the activity status changed by of the entity
        /// </summary>
        public Guid? ActivityStatusChangedBy { get; set; }
        /// <summary>
        /// gets or sets the activity status of the entity
        /// </summary>
        public ActivityStatus ActivityStatus { get; set; }
    }
}
