using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Commands.Create
{
    public class CreateGroupCommand : IRequest
    {
        public GroupDetailModel Model { get; set; }

        public class Handler : IRequestHandler<CreateGroupCommand, Unit>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMediator mediator;

            public Handler(IBillsMonsterDbContext dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
            {
                var entity = new Domain.Entities.Group()
                {
                    Title = request.Model.Title,
                    Description = request.Model.Description,
                    ParentId = request.Model.ParentId
                };

                dbContext.Groups.Add(entity);
                await dbContext.SaveChangesAsync(cancellationToken);
                //await mediator.Publish(new GroupCreated() { GroupId = entity.Id }, cancellationToken);
                await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.CREATE, nameof(Group))
                {
                    Id = entity.Id,
                    Title = entity.Title
                }, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
