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

namespace BillsMonster.Application.Bills.Commands.Delete
{
    public class DeleteBillCommand : IRequest
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteBillCommand, Unit>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMediator mediator;

            public Handler(IBillsMonsterDbContext dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }
            public async Task<Unit> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
            {
                var entity = await dbContext.Bills.FindAsync(request.Id);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Bill), request.Id);
                }
                dbContext.Bills.Remove(entity);
                await dbContext.SaveChangesAsync(cancellationToken);
                await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.DELETE,
                    nameof(Bill))
                {
                    Id = request.Id,
                    Title = entity.Title
                }, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
