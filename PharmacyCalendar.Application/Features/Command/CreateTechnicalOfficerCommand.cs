using AutoMapper;
using FluentValidation;
using MediatR;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts;

namespace PharmacyCalendar.Application.Features.Command
{
    public class CreateTechnicalOfficerCommand : IRequest<Guid>
    {
        public string FullName { get; set; }
        public string NationalCode { get; set; }


        #region [- Handler() -]
        public class Handler : IRequestHandler<CreateTechnicalOfficerCommand, Guid>
        {
            private readonly ITechnicalOfficerRepository _repository;
            public Handler(ITechnicalOfficerRepository repository)
            {
                _repository = repository;
            }
            public async Task<Guid> Handle(CreateTechnicalOfficerCommand request, CancellationToken cancellationToken)
            {
                var officer = new TechnicalOfficer(request.FullName, request.NationalCode);
                await _repository.AddAsync(officer, cancellationToken);
                await _repository.SaveChangeAsync(cancellationToken);
                return officer.Id;
            }
        }
        #endregion

        #region [- Validator() -]
        public class CreateCommandValidator : AbstractValidator<CreateTechnicalOfficerCommand>
        {
            public CreateCommandValidator()
            {
                RuleFor(c => c.FullName)
               .NotEmpty().WithMessage("لطفا نام و نام خانوادگی خود را وارد کنید")
               .NotNull().MaximumLength(50).WithMessage("نام و نام خانوادگی نباید بیشتر از 50 کاراکتر باشد");

                RuleFor(c => c.NationalCode)
               .NotEmpty().WithMessage("لطفا کدملی خود را وارد کنید")
               .Length(10).WithMessage("کد ملی باید ۱۰ رقم باشد.")
               .Must(BeAllDigits).WithMessage("کد ملی باید فقط شامل اعداد باشد.")
               .Must(BeValidNationalCode).WithMessage("کد ملی معتبر نیست.");
            }

            private bool BeAllDigits(string nationalCode)
            {
                return nationalCode.All(char.IsDigit);
            }

            private bool BeValidNationalCode(string nationalCode)
            {
                if (nationalCode.Length != 10 || !nationalCode.All(char.IsDigit))
                    return false;

                var check = int.Parse(nationalCode[9].ToString());
                var sum = 0;
                for (var i = 0; i < 9; i++)
                {
                    sum += int.Parse(nationalCode[i].ToString()) * (10 - i);
                }
                var remainder = sum % 11;
                return (remainder < 2 && check == remainder) || (remainder >= 2 && check == 11 - remainder);
            }
        }
        # endregion

    }
}
