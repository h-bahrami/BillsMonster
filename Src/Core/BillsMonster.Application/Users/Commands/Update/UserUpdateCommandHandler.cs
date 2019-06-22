using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Domain.Entities;
using BillsMonster.Application.Interfaces.Data;
using AutoMapper;
using BillsMonster.Application.Notifications;

namespace UsersMonster.Application.Users.Commands.Update
{
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, Unit>
    {
        private readonly IUsersRepository usersRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UserUpdateCommandHandler(IUsersRepository repo, IMediator mediator, IMapper mapper)
        {
            this.usersRepository = repo;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var mappedObj = mapper.Map<User>(request.Model);
            var notifStatus = await usersRepository.UpdateAsync(mappedObj) switch
            {
                true => NotificationStatus.SUCCEED,
                false => NotificationStatus.FAILED
            };

            await mediator.Publish(new EntityCommandsNotification(NotificationActionType.UPDATE, nameof(User), notifStatus)
            {
                Id = mappedObj.Id,
                Title = mappedObj.Email.Substring(0, 6)
            }, cancellationToken);

            return Unit.Value;

        }
    }
}

