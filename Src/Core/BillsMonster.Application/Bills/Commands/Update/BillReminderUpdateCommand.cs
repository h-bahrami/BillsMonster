using BillsMonster.Application.Exceptions;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Bills.Commands.Update
{
    public class BillReminderUpdateCommand : IRequest
    {
        public Guid ReminderId { get; set; }
        public string Title { get; set; }
        public ReminderType Type { get; set; }
        public DateTime AlarmTime { get; set; }

        public class Handler : IRequestHandler<BillReminderUpdateCommand, Unit>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMediator mediator;

            public Handler(IBillsMonsterDbContext dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(BillReminderUpdateCommand request, CancellationToken cancellationToken)
            {
                var entity = await dbContext.Reminders.FindAsync(request.ReminderId);

                if(entity == null)
                {
                    throw new NotFoundException(nameof(Reminder), request.ReminderId);
                }



                entity.Title = request.Title;
                entity.AlarmTime = request.AlarmTime;
                entity.Type = request.Type;

                dbContext.Reminders.Update(entity);
                await dbContext.SaveChangesAsync(cancellationToken);
                await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.UPDATE,
                    nameof(Bill))
                {
                    Id = entity.Id,
                    Title = entity.Title
                }, cancellationToken);

                return Unit.Value;

            }
        }
    }
}
