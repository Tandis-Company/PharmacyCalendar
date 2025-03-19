using Utilities.Framework;
using Utilities.Framework.Contracts;
using Utilities.Framework.Guards;

namespace PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate
{
    public class TechnicalOfficer : BaseEntity, IDbSetEntity
    {
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public List<TechniacalOfficerWorkShift> TechniacalOfficerWorkShift { get; set; }

        public TechnicalOfficer(string fullName, string nationalCode)
        {
            Guard.AgainstNullOrEmpty(fullName, "نام و نام خانوادگی الزامی است");
            Guard.AgainstNullValue(nationalCode, "کد ملی الزامی است");
            FullName = fullName;
            NationalCode = nationalCode;
        }
    }
}
