using Microsoft.EntityFrameworkCore;
using System.Threading;
using BillsMonster.Domain.Entities;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace BillsMonster.Application.Interfaces
{
    public interface IBillsMonsterDbContext
    {

        IMongoCollection<Bill> Bills { get; set;}

        IMongoCollection<Group> Groups{ get; set; }

        IMongoCollection<User> Users { get; set; }

        IMongoCollection<Reminder> Reminders { get; set;}


        //DbSet<Bill> Bills {get; set; }

        //DbSet<Group> Groups{ get; set; }

        //DbSet<User> Users { get; set; }

        //DbSet<Reminder> Reminders { get; set;}

        //Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
