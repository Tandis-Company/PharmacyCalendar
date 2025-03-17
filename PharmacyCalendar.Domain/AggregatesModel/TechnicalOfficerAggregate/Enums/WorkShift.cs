using System.ComponentModel.DataAnnotations;

namespace PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Enums
{
    public enum WorkShift
    {
        [Display(Name = "08:00 to 09:00")]
        _08_To_09 = 0,

        [Display(Name = "09:00 to 10:00")]
        _09_To_10 = 1,

        [Display(Name = "10:00 to 11:00")]
        _10_To_11 = 2,

        [Display(Name = "11:00 to 12:00")]
        _11_To_12 = 3,

        [Display(Name = "12:00 to 13:00")]
        _12_To_13 = 4,
    }
}
