using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Bills.Commands.Create
{
    public class CreateBillCommand : IRequest
    {
        public BillModel Model { get; set; }

        public class Handler : IRequestHandler<CreateBillCommand, Unit>
        {
            private readonly IBillsMonsterDbContext dbContext;
            private readonly IMediator mediator;
            // private readonly IMapper mapper;

            public Handler(IBillsMonsterDbContext dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }
            public async Task<Unit> Handle(CreateBillCommand request, CancellationToken cancellationToken)
            {
                var entity = (Bill) request.Model; // mapper.Map<BillModel, Bill>(request.Model);
                dbContext.Bills.Add(entity);
                await dbContext.SaveChangesAsync(cancellationToken);
                await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.CREATE, nameof(Bill))
                {
                    Id = entity.Id,
                    Title = entity.Title
                }, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
