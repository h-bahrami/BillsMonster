using AutoMapper;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace UsersMonster.Application.Users.Commands.Create
{
    public class CreateUserCommand : IRequest
    {
        public UserModel Model { get; set; }

        public class Handler : IRequestHandler<CreateUserCommand, Unit>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMediator mediator;
            // private readonly IMapper mapper;

            public Handler(IBillsMonsterDbContext dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }
            public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var entity = (User) request.Model; // mapper.Map<UserModel, User>(request.Model);
                dbContext.Users.Add(entity);
                await dbContext.SaveChangesAsync(cancellationToken);
                await mediator.Publish(new EntityCommandsNotification(BillsMonster.Application.Notifications.NotificationActionType.CREATE, nameof(User))
                {
                    Id = entity.Id,
                    Title = entity.Email.Substring(0,5)
                }, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
