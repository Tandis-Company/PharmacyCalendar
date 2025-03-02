using AutoMapper;
using MediatR;
using PharmacyCalendar.Application.Features.Dtos;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts;
using Utilities.Framework.Exceptions;

namespace PharmacyCalendar.Application.Features.Query
{
    public class GetTechnicalOfficerByIdQuery : IRequest<TechnicalOfficeroutputDto>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetTechnicalOfficerByIdQuery, TechnicalOfficeroutputDto>
        {
            private readonly IMapper _mapper;
            private readonly ITechnicalOfficerRepository _repository;

            public Handler(IMapper mapper, ITechnicalOfficerRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<TechnicalOfficeroutputDto> Handle(GetTechnicalOfficerByIdQuery query, CancellationToken cancellationToken)
            {
                if (query.Id == Guid.Empty)
                {
                    throw new AppException("کاربر یافت نشد");
                }
                var officer = await _repository.GetByIdAsync(query.Id, cancellationToken);
                if (officer == null)
                {
                    throw new AppException("مسئول فنی پیدا نشد");
                }
                return _mapper.Map<TechnicalOfficeroutputDto>(officer);
            }
        }
    }
}

