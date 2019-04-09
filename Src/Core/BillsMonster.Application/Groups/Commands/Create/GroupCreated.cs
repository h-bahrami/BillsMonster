using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Notifications.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Commands.Create
{
    public class GroupCreated: INotification
    {
        public Guid GroupId { get; set;}

        public class GroupCreatedHandler : INotificationHandler<GroupCreated>
        {
            private readonly INotificationService _notification;

            public GroupCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(GroupCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new Message());
            }
        }
    }
}
