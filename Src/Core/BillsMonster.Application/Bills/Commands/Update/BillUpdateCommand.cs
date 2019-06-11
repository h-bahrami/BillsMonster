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
    public class BillUpdateCommand : IRequest
    {
        public BillDto Model { get; set; }

        public class Handler : IRequestHandler<BillUpdateCommand, Unit>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMediator mediator;

            public Handler(IBillsMonsterDbContext dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(BillUpdateCommand request, CancellationToken cancellationToken)
            {
                var entity = await dbContext.Bills.FindAsync(request.Model.Id);

                entity.Title = request.Model.Title;
                entity.ReferenceId = request.Model.ReferenceId;
                entity.ReceivedAt = request.Model.ReceivedAt;
                entity.PaidAt = request.Model.PaidAt;
                entity.Note = request.Model.Note;
                entity.Image = request.Model.Image;
                entity.GroupId = request.Model.GroupId;
                entity.DueDate = request.Model.DueDate;
                entity.Amount = request.Model.Amount;

                dbContext.Bills.Update(entity);
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
