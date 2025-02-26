using AutoMapper;
using PharmacyCalendar.Application.Features.Command;
using PharmacyCalendar.Application.Features.Command.Dtos;
using PharmacyCalendar.Domain.AggregatesModel.GroupAggregate;

namespace PharmacyCalendar.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TechnicalOfficer, CreateoutputDto>().ReverseMap();
            CreateMap<TechnicalOfficer, CreateTechnicalOfficerCommand>().ReverseMap();
            //CreateMap<TechnicalOfficer, DeleteTechnicalOfficerD>().ReverseMap();
        }
    }
}
