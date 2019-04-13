using BillsMonster.Application.Exceptions;
using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Commands.Delete
{
    public class DeleteGroupCommand : IRequest
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteGroupCommand, Unit>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMediator mediator;

            public Handler(IBillsMonsterDbContext dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
            {
                var entity = await dbContext.Groups.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Group), request.Id);
                }

                var hasOpenBills = entity.Bills.Any(x => !x.PaidAt.HasValue);
                if (hasOpenBills)
                {
                    throw new DeleteFailureException(nameof(Group), request.Id, $"There is/are unpaid bills in this group");
                }

                dbContext.Groups.Remove(entity);

                await dbContext.SaveChangesAsync(cancellationToken);
                await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.DELETE, nameof(Group))
                {
                    Title = entity.Title,
                    Id = entity.Id
                }, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
