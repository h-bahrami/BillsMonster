using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Exceptions;
using BillsMonster.Domain.Entities;

namespace UsersMonster.Application.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteUserCommand, Unit>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMediator mediator;

            public Handler(IBillsMonsterDbContext dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }
            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var entity = await dbContext.Users.FindAsync(request.Id);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(User), request.Id);
                }
                dbContext.Users.Remove(entity);
                await dbContext.SaveChangesAsync(cancellationToken);
                await mediator.Publish(new EntityCommandsNotification(BillsMonster.Application.Notifications.NotificationActionType.DELETE,
                    nameof(User))
                {
                    Id = request.Id,
                    Title = entity.Email.Substring(0,5)
                }, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
