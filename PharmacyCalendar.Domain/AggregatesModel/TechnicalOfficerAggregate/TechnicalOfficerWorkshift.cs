using Utilities.Framework.Contracts;
using Utilities.Framework;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Enums;

namespace PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate
{
    public class TechnicalOfficerWorkshift : AggregateRoot, IDbSetEntity
    {
        public TechnicalOfficer FullName { get; set; }
        public WorkShift WorkShift { get; set; }
        public Weekdays Weekdays { get; set; }
    }
}
