using AutoMapper;
using BillsMonster.Application.Groups.Commands;
using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Interfaces.Data;
using BillsMonster.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace UsersMonster.Application.Users.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUsersRepository usersRepository;
        private readonly IMediator mediator;
        // private readonly IMapper mapper;

        public CreateUserCommandHandler(IUsersRepository repo, IMediator mediator)
        {
            this.usersRepository = repo;
            this.mediator = mediator;
        }
        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = (User) request.Model; // mapper.Map<UserModel, User>(request.Model);
            await usersRepository.InsertAsync(entity);            
            await mediator.Publish(new EntityCommandsNotification(BillsMonster.Application.Notifications.NotificationActionType.CREATE, nameof(User))
            {
                Id = entity.Id,
                Title = entity.Email.Substring(0, 5)
            }, cancellationToken);
            return Unit.Value;
        }
    }
}

