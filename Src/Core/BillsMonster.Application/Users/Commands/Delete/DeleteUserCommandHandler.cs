using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Domain.Entities;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Application.Notifications;

namespace UsersMonster.Application.Users.Commands.Delete
{
    public partial class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUsersRepository usersRepository;
        private readonly IMediator mediator;

        public DeleteUserCommandHandler(IUsersRepository repo, IMediator mediator)
        {
            this.usersRepository = repo;
            this.mediator = mediator;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var notifStatus = await usersRepository.DeleteAsync(request.Id) switch
            {
                true => NotificationStatus.SUCCEED,
                false => NotificationStatus.FAILED
            };

            await mediator.Publish(
                new EntityCommandsNotification(NotificationActionType.DELETE,
                nameof(User), notifStatus)
                {
                    Id = request.Id,
                    Title = ""
                }, cancellationToken);
            return Unit.Value;
        }
    }
}

