using AutoMapper;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Bills.Commands.Create
{
    public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, Guid>
    {
        private readonly IBillsRepository billsRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateBillCommandHandler(IBillsRepository repository, IMediator mediator, IMapper mapper)
        {
            this.billsRepository = repository;
            this.mediator = mediator;
            this.mapper = mapper;
        }
        public async Task<Guid> Handle(CreateBillCommand request, CancellationToken cancellationToken)
        {
            var mappedObject = mapper.Map<Bill>(request);
            await billsRepository.InsertAsync(mappedObject);
            await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.CREATE, nameof(Bill), 
                Notifications.NotificationStatus.SUCCEED)
            {
                Id = mappedObject.Id,
                Title = mappedObject.Title
            }, cancellationToken);
            return mappedObject.Id;
        }
    }
}

