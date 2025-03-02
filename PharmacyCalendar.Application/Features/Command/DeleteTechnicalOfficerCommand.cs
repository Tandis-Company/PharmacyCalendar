using MediatR;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts;
using Utilities.Framework.Exceptions;

namespace PharmacyCalendar.Application.Features.Command
{
    public class DeleteTechnicalOfficerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteTechnicalOfficerCommand, bool>
        {
            private readonly IMediator _mediator;
            private readonly ITechnicalOfficerRepository _repository;
            public Handler(IMediator mediator, ITechnicalOfficerRepository repository)
            {
                _mediator = mediator;
                _repository = repository;
            }

            public async Task<bool> Handle(DeleteTechnicalOfficerCommand request, CancellationToken cancellationToken)
            {
                var officer = await _repository.GetByIdAsync(request.Id, cancellationToken);
                if (officer == null)
                {
                    throw new AppException("گروه پیدا نشد", System.Net.HttpStatusCode.NotFound);
                }
                await _repository.DeleteAsync(officer);
                await _repository.SaveChangeAsync();
                if (await _repository.DeleteAsync(officer, cancellationToken) != null)
                    return true;
                return false;
            }
        }
    }
}