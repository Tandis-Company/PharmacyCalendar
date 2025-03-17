using MediatR;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts;
using Utilities.Framework.Exceptions;

namespace PharmacyCalendar.Application.Features.Command
{
    public class DeleteTechnicalOfficerCommand : IRequest<Empty>
    {
        public Guid Id { get; set; }

        #region [- Handler() -]
        public class Handler : IRequestHandler<DeleteTechnicalOfficerCommand, Empty>
        {
            private readonly ITechnicalOfficerRepository _repository;
            public Handler(ITechnicalOfficerRepository repository)
            {
                _repository = repository;
            }

            public async Task<Empty> Handle(DeleteTechnicalOfficerCommand request, CancellationToken cancellationToken)
            {
                var officer = await _repository.GetByIdAsync(request.Id, cancellationToken);
                if (officer == null)
                {
                    throw new AppException("کاربر یافت نشد", System.Net.HttpStatusCode.NotFound);
                }
                await _repository.DeleteAsync(officer,cancellationToken);
                await _repository.SaveChangeAsync(cancellationToken);
                return Empty.Instance;
            }
        }
        #endregion


    }
}