﻿using BillsMonster.Application.Exceptions;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Bills.Commands.Create
{
    public class CreateBillReminderCommandHandler
    {
        public class Handler : IRequestHandler<CreateBillReminderCommand, Unit>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMediator mediator;

            public Handler(IBillsMonsterDbContext dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(CreateBillReminderCommand request, CancellationToken cancellationToken)
            {
                
                var bill = await this.dbContext.Bills.FindAsync(new BsonDocument("BillId", request.BillId)); // .FindAsync(request.BillId);
                if (bill == null)
                {
                    throw new NotFoundException(nameof(Bill), request.BillId);
                }



                bill.Reminders.Add(new Reminder()
                {
                    Text = request.Title,
                    AlarmTime = request.AlarmTime,
                    Type = request.Type
                });

                await dbContext.SaveChangesAsync(cancellationToken);
                await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.CREATE, nameof(Reminder))
                {
                    Id = request.BillId,
                    Title = request.Title
                }, cancellationToken);
                return Unit.Value;

            }
        }
    }
}
