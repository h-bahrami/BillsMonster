using MediatR;

namespace UsersMonster.Application.Users.Commands.Create
{
    public partial class CreateUserCommand : IRequest
    {
        public UserModel Model { get; set; }
    }
}
