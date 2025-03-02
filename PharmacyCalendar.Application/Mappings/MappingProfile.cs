using AutoMapper;
using PharmacyCalendar.Application.Features.Command;
using PharmacyCalendar.Application.Features.Dtos;
using PharmacyCalendar.Application.Features.Query;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate;

namespace PharmacyCalendar.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TechnicalOfficer, TechnicalOfficeroutputDto>().ReverseMap();
            CreateMap<TechnicalOfficer, CreateTechnicalOfficerCommand>().ReverseMap();
            CreateMap<TechnicalOfficer, DeleteTechnicalOfficerCommand>().ReverseMap();

            CreateMap<TechnicalOfficeroutputDto, CreateTechnicalOfficerCommand>().ReverseMap();
            CreateMap<TechnicalOfficeroutputDto, GetTechnicalOfficerByIdQuery>().ReverseMap();
        }
    }
}
