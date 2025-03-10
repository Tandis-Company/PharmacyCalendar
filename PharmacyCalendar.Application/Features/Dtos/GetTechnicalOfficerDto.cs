using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Enums;

namespace PharmacyCalendar.Application.Features.Dtos
{
    public class GetTechnicalOfficerDto
    {
        public string FullName { get; set; }
        public string NationalCode { get; set; }
    }
}
