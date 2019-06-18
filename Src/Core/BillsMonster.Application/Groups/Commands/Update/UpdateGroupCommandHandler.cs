using AutoMapper;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Application.Notifications;
using BillsMonster.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Commands.Update
{
    public partial class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Unit>
    {
        private readonly IGroupsRepository repo;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UpdateGroupCommandHandler(IGroupsRepository repo, IMediator mediator, IMapper mapper)
        {
            this.repo = repo;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var mappedObj = mapper.Map<Group>(request);
            var notifStatus = await repo.UpdateAsync(mappedObj) switch
            {
                true => NotificationStatus.SUCCEED,
                false => NotificationStatus.FAILED
            };

            await mediator.Publish(new EntityCommandsNotification(
                Notifications.NotificationActionType.UPDATE, 
                nameof(Group),
                notifStatus)
            {
                Id = mappedObj.Id,
                Title = mappedObj.Title
            }, cancellationToken);
            return Unit.Value;
        }
    }
}

