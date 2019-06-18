using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Application.Notifications;
using BillsMonster.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Bills.Commands.Delete
{
    public class DeleteBillCommandHandler : IRequestHandler<DeleteBillCommand, Unit>
    {
        private readonly IBillsRepository billsRepository;
        private readonly IMediator mediator;

        public DeleteBillCommandHandler(IBillsRepository billsRepository, IMediator mediator)
        {
            this.billsRepository = billsRepository;
            this.mediator = mediator;
        }
        public async Task<Unit> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
        {
            var notifStatus = await this.billsRepository.DeleteAsync(request.Id) switch
            {
                true => NotificationStatus.SUCCEED,
                false => NotificationStatus.FAILED
            };
            await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.DELETE,
                nameof(Bill), notifStatus)
            {
                Id = request.Id,
                Title = ""
            }, cancellationToken);
            return Unit.Value;
        }
    }
}

