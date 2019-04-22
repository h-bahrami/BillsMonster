using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Domain.Entities;

namespace UsersMonster.Application.Users.Commands.Update
{
    public class UserUpdateCommand : IRequest
    {
        public UserModel Model { get; set; }

        public class Handler : IRequestHandler<UserUpdateCommand, Unit>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMediator mediator;

            public Handler(IBillsMonsterDbContext dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
            {
                var entity = await dbContext.Users.FindAsync(request.Model.Id);

                entity.Email = request.Model.Email;

                dbContext.Users.Update(entity);
                await dbContext.SaveChangesAsync(cancellationToken);
                await mediator.Publish(new EntityCommandsNotification(BillsMonster.Application.Notifications.NotificationActionType.UPDATE, nameof(User))
                {
                    Id = entity.Id,
                    Title = entity.Email.Substring(0, 6)
                }, cancellationToken);

                return Unit.Value;

            }
        }
    }
}
