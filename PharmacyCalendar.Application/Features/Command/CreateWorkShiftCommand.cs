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
        public List<WorkShift> WorkShift { get; set; }
        public List<Weekdays> Weekdays { get; set; }
        public Guid TechnicalOfficerId { get; set; }

        #region [- Handler() -]
        public class Handler : IRequestHandler<CreateWorkShiftCommand, List<CreateWorkShiftDto>>
        {
            private readonly ITechnicalOfficerRepository _repository;
            public Handler(ITechnicalOfficerRepository repository)
            {
                _repository = repository;
            }
            public async Task<List<CreateWorkShiftDto>> Handle(CreateWorkShiftCommand request, CancellationToken cancellationToken)
            {
                var technicalOfficer = await _repository.GetByIdAsync(request.TechnicalOfficerId, cancellationToken);

                if (technicalOfficer == null)
                {
                    throw new Exception("TechnicalOfficer not found.");
                }

                var workShifts = new List<TechniacalOfficerWorkShift>();
                foreach (var weekday in request.Weekdays)
                {
                    foreach (var workShift in request.WorkShift)
                    {
                        workShifts.Add(new TechniacalOfficerWorkShift
                        {
                            WorkShift = workShift,
                            Weekdays = weekday,
                            TechnicalOfficerId = request.TechnicalOfficerId,
                        });
                    }
                }

                await _repository.AddRangeAsync(workShifts, cancellationToken);

                return workShifts.Select(ws => new CreateWorkShiftDto
                {
                    WorkShift = ws.WorkShift,
                    Weekdays = ws.Weekdays,
                    TechnicalOfficerId = ws.TechnicalOfficerId,
                    FullName = technicalOfficer.FullName
                }).ToList();
            }
        }
        #endregion

        #region [- Validator() -]

        public class CreateWorkShiftCommandValidator : AbstractValidator<CreateWorkShiftCommand>
        {
            public CreateWorkShiftCommandValidator()
            {
                RuleFor(x => x.Weekdays)
                    .NotEmpty().WithMessage("روز هفته باید مشخص شود.")
                    .Must(weekdays => weekdays.All(w => Enum.IsDefined(typeof(Weekdays), w)))
                    .WithMessage("روز هفته نامعتبر است");

                RuleFor(x => x.WorkShift)
                      .NotEmpty().WithMessage("شیفت کاری باید مشخص شود.")
                      .Must(shifts => shifts.All(s => Enum.IsDefined(typeof(WorkShift), s)))
                      .WithMessage("شیفت کاری نامعتبر است")
                      .Must(HaveConsecutiveShifts)
                      .WithMessage("حداقل ۴ شیفت متوالی باید انتخاب شود.");
            }

            private bool HaveConsecutiveShifts(List<WorkShift> workShifts)
            {
                if (workShifts == null || workShifts.Count < 4)
                    return false;

                var sortedShifts = workShifts.OrderBy(ws => (int)ws).ToList();

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
                return false;
            }
        }

        #endregion

    }
}

