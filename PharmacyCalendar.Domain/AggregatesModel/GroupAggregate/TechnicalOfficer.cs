using PharmacyCalendar.Domain.AggregatesModel.GroupAggregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Framework;
using Utilities.Framework.Contracts;
using Utilities.Framework.Guards;

namespace PharmacyCalendar.Domain.AggregatesModel.GroupAggregate
{
    public class TechnicalOfficer: AggregateRoot, IAuditable<int>, IDbSetEntity
    {
        public string FullName { get; private set; }
        public string NationalCode { get; private set; }
        public WorkShift WorkShift { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public int LastModifiedBy { get; private set; }
        public DateTime? LastModifiedDate { get; private set; }

        public TechnicalOfficer(string fullName, string nationalCode)
        {
            Guard.AgainstNullOrEmpty(fullName, "نام و نام خانوادگی الزامی است");
            Guard.AgainstNullValue(nationalCode, "کد ملی الزامی است");
            FullName = fullName;
            NationalCode = nationalCode;
        }
    }
}
