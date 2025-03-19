using Utilities.Framework.Contracts;
using Utilities.Framework;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Enums;

namespace PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate
{
    public class TechniacalOfficerWorkShift : BaseEntity, IDbSetEntity
    {
        public WorkShift WorkShift { get; set; }
        public Weekdays Weekdays { get; set; }
        public Guid TechnicalOfficerId { get; set; }
        public TechnicalOfficer TechnicalOfficer { get; set; }
    }
}
