using System.Threading.Tasks;

namespace BillsMonster.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Notifications.Models.Message message);
    }
}