//using FluentValidation;
//using MediatR;
//using PharmacyCalendar.Application.Features.Dtos.WorkShiftDto;
//using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate;
//using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts;
//using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Enums;

//namespace PharmacyCalendar.Application.Features.Command
//{
//    public class CreateWorkShiftCommand : IRequest<List<CreateWorkShiftDto>>
//    {
//        public WorkShift WorkShift { get; set; }

//        #region [- Handler() -]

//        public class Handler : IRequestHandler<CreateWorkShiftCommand, List<CreateWorkShiftDto>>
//        {
//            private readonly ITechnicalOfficerRepository _repository;
//            public Handler(ITechnicalOfficerRepository repository)
//            {
//                _repository = repository;
//            }
//            public async Task<List<CreateWorkShiftDto>> Handle(CreateWorkShiftCommand request, CancellationToken cancellationToken)
//            {
//                var officer = new TechnicalOfficer("نام کامل نمونه", "کد ملی نمونه");
//                officer.Weekdays = request.WorkShift.Weekdays;
//                officer.workShift = request.WorkShift;

//                await _repository.AddAsync(officer, cancellationToken);
//                var officerDto = new CreateWorkShiftDto
//                {
//                    FullName = officer.FullName,
//                    Weekdays = officer.Weekdays,
//                    WorkShift = officer.workShift
//                };

//                return new List<CreateWorkShiftDto> { officerDto };
//            }
//        }

//        #endregion

//        #region [- Validator() -]

//        public class CreateWorkShiftCommandValidator : AbstractValidator<CreateWorkShiftCommand>
//        {
//            public CreateWorkShiftCommandValidator()
//            {
//                RuleFor(c => c.WorkShift)
//               .NotEmpty().WithMessage("لطفا شیفت کاری خود را وارد کنید");
//            }
//        }

//        #endregion

//    }
//}
