using BillsMonster.Application.Exceptions;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Bills.Commands.Delete
{
    public class DeleteBillReminderCommand: IRequest
    {
        public Guid ReminderId { get; set; }

        public class Handler : IRequestHandler<DeleteBillReminderCommand, Unit>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMediator mediator;

            public Handler(IBillsMonsterDbContext dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }
            public async Task<Unit> Handle(DeleteBillReminderCommand request, CancellationToken cancellationToken)
            {
                var reminder = await dbContext.Reminders.FindAsync(request.ReminderId);
                if (reminder == null)
                {
                    throw new NotFoundException(nameof(Reminder), request.ReminderId);
                }

                var bill = await dbContext.Bills.FindAsync(reminder.BillId);
                if (bill == null)
                {
                    throw new NotFoundException(nameof(Bill), reminder.BillId);
                }

                bill.Reminders.Remove(reminder);
                await dbContext.SaveChangesAsync(cancellationToken);
                await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.DELETE,
                    nameof(Reminder))
                {
                    Id = request.ReminderId,
                    Title = reminder.Title
                }, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
