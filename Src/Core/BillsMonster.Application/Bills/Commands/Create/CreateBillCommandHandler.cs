using AutoMapper;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Bills.Commands.Create
{
    public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, Unit>
    {
        private readonly IBillsRepository dbContext;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateBillCommandHandler(IBillsRepository dbContext, IMediator mediator, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(CreateBillCommand request, CancellationToken cancellationToken)
        {
            var mappedObject = mapper.Map<Bill>(request);
            await dbContext.InsertAsync(mappedObject);
            await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.CREATE, nameof(Bill))
            {
                Id = mappedObject.Id,
                Title = mappedObject.Title
            }, cancellationToken);
            return Unit.Value;
        }
    }
}
}
