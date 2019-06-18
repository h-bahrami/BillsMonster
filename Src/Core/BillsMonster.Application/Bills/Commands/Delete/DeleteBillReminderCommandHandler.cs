using BillsMonster.Application.Exceptions;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Application.Notifications;
using BillsMonster.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Bills.Commands.Delete
{
    public class DeleteBillReminderCommandHandler : IRequestHandler<DeleteBillReminderCommand, Unit>
    {
        private readonly IBillsRepository billsRepository;
        private readonly IMediator mediator;

        public DeleteBillReminderCommandHandler(IBillsRepository repo, IMediator mediator)
        {
            this.billsRepository = repo;
            this.mediator = mediator;
        }
        public async Task<Unit> Handle(DeleteBillReminderCommand request, CancellationToken cancellationToken)
        {
            var notifStatus = await billsRepository.DeleteReminderAsync(request.BillId, request.ReminderId) switch
            {
                true => NotificationStatus.SUCCEED,
                false => NotificationStatus.FAILED
            };

            await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.DELETE,
                nameof(Reminder), notifStatus)
            {
                Id = request.ReminderId,
                Title = ""
            }, cancellationToken);
            return Unit.Value;
        }
    }
}

