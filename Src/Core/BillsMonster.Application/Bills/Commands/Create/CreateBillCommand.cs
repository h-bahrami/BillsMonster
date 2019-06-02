﻿using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Interfaces.Data;
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
            private readonly IBillsRepository dbContext;
            private readonly IMediator mediator;
            // private readonly IMapper mapper;

            public Handler(IBillsRepository dbContext, IMediator mediator)
            {
                this.dbContext = dbContext;
                this.mediator = mediator;
            }
            public async Task<Unit> Handle(CreateBillCommand request, CancellationToken cancellationToken)
            {
               await dbContext.InsertAsync(request.Model.);
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
