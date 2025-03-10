using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Enums
{
    public enum Weekdays
    {
        [Description("شنبه")]
        Saturday = 0,

        [Description("یکشنبه")]
        Sunday = 1,

        [Description("دوشنبه")]
        Monday = 2,

        [Description("سه‌شنبه")]
        Tuesday = 3,

        [Description("چهارشنبه")]
        Wednesday = 4,

        [Description("پنجشنبه")]
        Thursday = 5,

        [Description("جمعه")]
        Friday = 6
    }
}
