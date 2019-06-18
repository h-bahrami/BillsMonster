using AutoMapper;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Bills.Commands.Create
{
    public class CreateBillReminderCommandHandler
    {
        public class Handler : IRequestHandler<CreateBillReminderCommand, Unit>
        {
            private readonly IBillsRepository billsRepo;
            private readonly IMediator mediator;
            private readonly IMapper mapper;

            public Handler(IBillsRepository repo, IMediator mediator, IMapper mapper)
            {
                this.billsRepo = repo;
                this.mediator = mediator;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(CreateBillReminderCommand request, CancellationToken cancellationToken)
            {
                var reminder = mapper.Map<Reminder>(request);
                var notifStatus = await billsRepo.AddReminderAsync(request.BillId, reminder) switch
                {
                    true => Notifications.NotificationStatus.SUCCEED,
                    false => Notifications.NotificationStatus.FAILED
                };

                await mediator.Publish(
                    new EntityCommandsNotification(Notifications.NotificationActionType.CREATE,
                    nameof(Reminder),
                    notifStatus), cancellationToken);
                return Unit.Value;

            }
        }
    }
}
