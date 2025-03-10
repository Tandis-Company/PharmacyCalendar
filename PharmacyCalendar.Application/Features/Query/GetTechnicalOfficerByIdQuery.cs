using AutoMapper;
using MediatR;
using PharmacyCalendar.Application.Features.Dtos;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts;
using Utilities.Framework.Exceptions;

namespace PharmacyCalendar.Application.Features.Query
{
    public class GetTechnicalOfficerByIdQuery : IRequest<GetTechnicalOfficerDto>
    {
        public Guid Id { get; set; }

        #region [- Handler() -]
        public class Handler : IRequestHandler<GetTechnicalOfficerByIdQuery, GetTechnicalOfficerDto>
        {
            private readonly ITechnicalOfficerRepository _repository;
            public Handler(ITechnicalOfficerRepository repository)
            {
                _repository = repository;
            }
            public async Task<GetTechnicalOfficerDto> Handle(GetTechnicalOfficerByIdQuery query, CancellationToken cancellationToken)
            {
                if (query.Id == Guid.Empty)
                {
                    throw new AppException("کاربر یافت نشد");
                }
                var officer = await _repository.GetByIdAsync(query.Id, cancellationToken);
                if (officer == null)
                {
                    throw new AppException("مسئول فنی پیدا نشد", System.Net.HttpStatusCode.NotFound);
                }
                var officerDto = new GetTechnicalOfficerDto
                {
                    FullName = officer.FullName,
                    NationalCode = officer.NationalCode,
                };
                return officerDto;
            }
        }
        #endregion

    }
}

