using AutoMapper;
using FluentValidation;
using MediatR;
using PharmacyCalendar.Application.Features.Dtos;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts;

namespace PharmacyCalendar.Application.Features.Command
{
    public class CreateTechnicalOfficerCommand : IRequest<TechnicalOfficeroutputDto>
    {
        public string FullName { get; set; }
        public string NationalCode { get; set; }


        #region [- Handler() -]
        public class Handler : IRequestHandler<CreateTechnicalOfficerCommand, TechnicalOfficeroutputDto>
        {
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly ITechnicalOfficerRepository _repository;
            public Handler(IMapper mapper, IMediator mediator, ITechnicalOfficerRepository repository)
            {
                _mapper = mapper;
                _mediator = mediator;
                _repository = repository;
            }
            public async Task<TechnicalOfficeroutputDto> Handle(CreateTechnicalOfficerCommand request, CancellationToken cancellationToken)
            {
                var officer = new TechnicalOfficer(request.FullName, request.NationalCode);
                await _repository.AddAsync(officer, cancellationToken);
                await _repository.SaveChangeAsync(cancellationToken);
                return _mapper.Map<TechnicalOfficeroutputDto>(officer);
            }
        }
        #endregion

        #region [- Validator() -]
        public class CreateCommandValidator : AbstractValidator<CreateTechnicalOfficerCommand>
        {
            public CreateCommandValidator()
            {
                RuleFor(c => c.FullName)
               .NotEmpty().WithMessage("{FullName} is required")
               .NotNull().MaximumLength(200).WithMessage("{FullName} must not exceed 200 characters. ");

                RuleFor(c => c.NationalCode)
               .NotEmpty().WithMessage("{NationalCode} is required")
               .NotNull().MaximumLength(200).WithMessage("{NationalCode} must not exceed 200 characters. ");
            }
        }
        # endregion
    }
}
