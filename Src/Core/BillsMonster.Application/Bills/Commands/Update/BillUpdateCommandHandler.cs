using AutoMapper;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Application.Notifications;
using BillsMonster.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Bills.Commands.Update
{
    public class BillUpdateCommandHandler : IRequestHandler<BillUpdateCommand, Unit>
    {
        private readonly IBillsRepository billsRepo;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public BillUpdateCommandHandler(IBillsRepository repo, IMediator mediator, IMapper mapper)
        {
            this.billsRepo = repo;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(BillUpdateCommand request, CancellationToken cancellationToken)
        {
            var mappedObject = mapper.Map<Bill>(request);
            var notifStatus = await billsRepo.UpdateAsync(mappedObject) switch
            {
                true => NotificationStatus.SUCCEED,
                false => NotificationStatus.FAILED
            };
            await mediator.Publish(new EntityCommandsNotification(Notifications.NotificationActionType.UPDATE,
                nameof(Bill), notifStatus)
            {
                Id = mappedObject.Id,
                Title = mappedObject.Title
            }, cancellationToken);

            return Unit.Value;
        }
    }
}

