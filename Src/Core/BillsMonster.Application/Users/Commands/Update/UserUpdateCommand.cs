using MediatR;

namespace UsersMonster.Application.Users.Commands.Update
{
    public class UserUpdateCommand : IRequest
    {
        public UserModel Model { get; set; }
    }
}
