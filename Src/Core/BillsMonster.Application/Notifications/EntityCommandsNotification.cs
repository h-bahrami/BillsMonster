using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BillsMonster.Application.Groups.Commands
{
    public class EntityCommandsNotification : INotification
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public NotificationActionType ActionType { get; private set; }
        public string EntityName { get; private set;}
        public NotificationStatus Status { get; set; }

        public EntityCommandsNotification(NotificationActionType actionType, string entityName = "", NotificationStatus status = NotificationStatus.SUCCEED)
        {
            ActionType = actionType;
            EntityName = entityName;
            Status = status;
        }

        public class Handler : INotificationHandler<EntityCommandsNotification>
        {
            private readonly INotificationService notificationService;

            public Handler(INotificationService notificationService)
            {
                this.notificationService = notificationService;
            }

            public async Task Handle(EntityCommandsNotification notification, CancellationToken cancellationToken)
            {
                await this.notificationService.SendAsync(new Notifications.Models.Message()
                {
                    Subject = $"{notification.EntityName} {notification.ActionType} Status {notification.Status}",
                    Body = $"Action {notification.ActionType} on {notification.EntityName} {notification.Title} ({notification.Id})  done."
                });
            }
        }
    }
}
