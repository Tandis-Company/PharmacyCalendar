using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities.Framework;
using Utilities.Framework.Contracts;
using Utilities.Framework.Guards;

namespace PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate
{
    public class TechnicalOfficer : AggregateRoot, IDbSetEntity
    {
        public string FullName { get; private set; }
        public string NationalCode { get; private set; }
        public DateTime CreatedDate { get; private set; }


        public TechnicalOfficer(string fullName, string nationalCode, DateTime createdDate)
        {
            Guard.AgainstNullOrEmpty(fullName, "نام و نام خانوادگی الزامی است");
            Guard.AgainstNullValue(nationalCode, "کد ملی الزامی است");
            FullName = fullName;
            NationalCode = nationalCode;
            CreatedDate = createdDate;
        }
    }
}
