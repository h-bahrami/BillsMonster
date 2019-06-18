using BillsMonster.Application.Exceptions;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Application.Notifications;
using BillsMonster.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Commands.Delete
{
    public partial class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, Unit>
    {
        private readonly IGroupsRepository groupsRepo;
        private readonly IBillsRepository billsRepository;
        private readonly IMediator mediator;

        public DeleteGroupCommandHandler(IGroupsRepository groupsRepo, 
            IBillsRepository billsRepository, IMediator mediator)
        {
            this.groupsRepo = groupsRepo;
            this.billsRepository = billsRepository;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await groupsRepo.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }

            var hasOpenBills = await billsRepository.GetBillsByAsync(request.UserId, request.Id);
            if (hasOpenBills.Any())
            {
                throw new DeleteFailureException(nameof(Group), request.Id, $"There is/are unpaid bills in this group");
            }

            var notifStatus = await groupsRepo.DeleteAsync(request.Id) switch
            {
                true => NotificationStatus.SUCCEED,
                false => NotificationStatus.FAILED
            };

            await mediator.Publish(
                new EntityCommandsNotification(
                    Notifications.NotificationActionType.DELETE, nameof(Group), notifStatus)
                {
                    Title = entity.Title,
                    Id = entity.Id
                }, cancellationToken);

            return Unit.Value;
        }
    }
}

