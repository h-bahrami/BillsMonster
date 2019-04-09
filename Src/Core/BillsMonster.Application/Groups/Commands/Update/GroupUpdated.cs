using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Notifications.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Commands.Update
{
    public class GroupUpdated : INotification
    {
        public Guid GroupId { get; set; }
        public string GroupTitle { get; set; }

        public class GroupCreatedHandler : INotificationHandler<GroupUpdated>
        {
            private readonly INotificationService _notification;

            public GroupCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(GroupUpdated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new Message() { Body = $"Group {notification.GroupTitle} ({notification.GroupId}) updated." });
            }
        }
    }
}
