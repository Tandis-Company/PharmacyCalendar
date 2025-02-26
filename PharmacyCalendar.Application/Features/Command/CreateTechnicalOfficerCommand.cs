using AutoMapper;
using FluentValidation;
using MediatR;
using PharmacyCalendar.Application.Features.Command.Dtos;
using PharmacyCalendar.Domain.AggregatesModel.GroupAggregate;
using PharmacyCalendar.Domain.AggregatesModel.GroupAggregate.Contracts;
using System.Text.RegularExpressions;

namespace PharmacyCalendar.Application.Features.Command
{
    public class CreateTechnicalOfficerCommand : IRequest<CreateoutputDto>
    {
        public string FullName { get; set; }
        public string NationalCode { get; set; }


        #region [- Handler() -]
        public class Handler : IRequestHandler<CreateTechnicalOfficerCommand, CreateoutputDto>
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
            public async Task<CreateoutputDto> Handle(CreateTechnicalOfficerCommand request, CancellationToken cancellationToken)
            {
                var officer = new TechnicalOfficer(request.FullName, request.NationalCode);
                await _repository.AddAsync(officer, cancellationToken);
                await _mediator.Send(officer, cancellationToken);
                await _repository.SaveChangeAsync(cancellationToken);
                return _mapper.Map<CreateoutputDto>(officer);
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
