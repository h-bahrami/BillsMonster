using BillsMonster.Application.Exceptions;
using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Commands.Update
{
    public class UpdateGroupCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }

        public class Handler : IRequestHandler<UpdateGroupCommand, Unit>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMediator mediator;

            public Handler(IBillsMonsterDbContext dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
            {
                var entity = await dbContext.Groups.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Group), request.Id);
                }

                entity.ParentId = request.ParentId;
                entity.Title = request.Title;
                entity.Description = request.Description;

                dbContext.Groups.Update(entity);

                await dbContext.SaveChangesAsync(cancellationToken);
                // await mediator.Publish(new GroupUpdated() { GroupId = entity.Id, GroupTitle = entity.Title}, cancellationToken);
                await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.UPDATE, nameof(Group))
                {
                    Id = entity.Id,
                    Title = entity.Title
                }, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
