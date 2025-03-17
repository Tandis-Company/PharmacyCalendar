using FluentValidation;
using MediatR;
using PharmacyCalendar.Application.Features.Dtos.WorkShiftDto;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Enums;

namespace PharmacyCalendar.Application.Features.Command
{
    public class CreateWorkShiftCommand : IRequest<CreateWorkShiftDto>
    {
        public WorkShift WorkShift { get; set; }
        public Weekdays Weekdays { get; set; }
        public Guid TechnicalOfficerId { get; set; }


        #region [- Handler() -]
        public class Handler : IRequestHandler<CreateWorkShiftCommand,CreateWorkShiftDto>
        {
            private readonly ITechnicalOfficerRepository _repository;
            public Handler(ITechnicalOfficerRepository repository)
            {
                _repository = repository;
            }
            public async Task<CreateWorkShiftDto> Handle(CreateWorkShiftCommand request, CancellationToken cancellationToken)
            {
                var technicalOfficer = await _repository.GetByIdAsync(request.TechnicalOfficerId, cancellationToken);

                if (technicalOfficer == null)
                {
                    throw new Exception("TechnicalOfficer not found.");
                }

                var officer = new TechniacalOfficerWorkShift
                {
                    Weekdays = request.Weekdays,
                    WorkShift = request.WorkShift,
                    TechnicalOfficerId = request.TechnicalOfficerId,
                };

                await _repository.AddAsync(officer, cancellationToken);
                var officerDto = new CreateWorkShiftDto
                {
                    Weekdays = officer.Weekdays,
                    WorkShift = officer.WorkShift,
                    TechnicalOfficerId = officer.TechnicalOfficerId,
                    FullName = technicalOfficer.FullName
                };
                return officerDto;
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

