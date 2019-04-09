using Microsoft.EntityFrameworkCore;
using System.Threading;
using BillsMonster.Domain.Entities;
using System.Threading.Tasks;

namespace BillsMonster.Application.Interfaces
{
    public interface IBillsMonsterDbContext
    {
        DbSet<Bill> Bills {get; set; }

        DbSet<Group> Groups{ get; set; }

        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
