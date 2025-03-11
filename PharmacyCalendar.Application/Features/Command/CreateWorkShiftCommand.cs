using FluentValidation;
using MediatR;
using PharmacyCalendar.Application.Features.Dtos.WorkShiftDto;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Enums;

namespace PharmacyCalendar.Application.Features.Command
{
    public class CreateWorkShiftCommand : IRequest<List<CreateWorkShiftDto>>
    {
        public WorkShift WorkShift { get; set; }
        public Weekdays Weekdays { get; set; }

        #region [- Handler() -]

        public class Handler : IRequestHandler<CreateWorkShiftCommand, List<CreateWorkShiftDto>>
        {
            private readonly ITechnicalOfficerWorkShiftRepository _repository;
            public Handler(ITechnicalOfficerWorkShiftRepository repository)
            {
                _repository = repository;
            }
            public async Task<List<CreateWorkShiftDto>> Handle(CreateWorkShiftCommand request, CancellationToken cancellationToken)
            {
                var officer = new TechnicalOfficerWorkshift();
                officer.Weekdays = request.Weekdays;
                officer.WorkShift = request.WorkShift;

                await _repository.AddAsync(officer, cancellationToken);
                var officerDto = new CreateWorkShiftDto
                {
                    Weekdays = officer.Weekdays,
                    WorkShift = officer.WorkShift,
                    FullName = officer.FullName.FullName,
                };

                return new List<CreateWorkShiftDto> { officerDto };
            }
        }

        #endregion

        #region [- Validator() -]
        public class CreateWorkShiftDtoValidator : AbstractValidator<CreateWorkShiftCommand>
        {
            public CreateWorkShiftDtoValidator()
            {

                RuleFor(x => x.Weekdays)
                    .IsInEnum().WithMessage("روز هفته نامعتبر است.");

                RuleFor(x => x.WorkShift)
                    .IsInEnum().WithMessage("شیفت کاری نامعتبر است.");
            }
        }
        public class CreateWorkShiftCollectionValidator : AbstractValidator<List<CreateWorkShiftCommand>>
        {
            public CreateWorkShiftCollectionValidator()
            {
                RuleFor(x => x)
                    .Must(HaveConsecutiveShifts)
                    .WithMessage("حداقل ۴ شیفت متوالی باید انتخاب شود.");
            }

            private bool HaveConsecutiveShifts(List<CreateWorkShiftCommand> shifts)
            {
                if (shifts == null || shifts.Count < 4)
                    return false;

                var groupedShifts = shifts
                    .GroupBy(s => new { s.Weekdays })
                    .ToList();

                foreach (var group in groupedShifts)
                {
                    var sortedShifts = group
                        .OrderBy(s => s.WorkShift)
                        .Select(s => s.WorkShift)
                        .ToList();

                    int consecutiveCount = 1;
                    for (int i = 1; i < sortedShifts.Count; i++)
                    {
                        if ((int)sortedShifts[i] == (int)sortedShifts[i - 1] + 1)
                        {
                            consecutiveCount++;
                            if (consecutiveCount >= 4)
                                return true;
                        }
                        else
                        {
                            consecutiveCount = 1;
                        }
                    }
                }
                return false;
            }
        }
        #endregion
    }
}

