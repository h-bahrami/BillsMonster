using BillsMonster.Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Commands.Create
{
    public class CreateGroupCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }

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
                    Title = request.Title,
                    Description = request.Description,
                    ParentId = request.ParentId
                };

                dbContext.Groups.Add(entity);
                await dbContext.SaveChangesAsync(cancellationToken);
                await mediator.Publish(new GroupCreated() { GroupId = entity.Id }, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
