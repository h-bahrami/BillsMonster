using BillsMonster.Application.Interfaces;
using BillsMonster.Application.Notifications.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillsMonster.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
