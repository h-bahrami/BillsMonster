using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillsMonster.Persistence
{
    public class BillsMonsterDbContext : DbContext, IBillsMonsterDbContext
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set;}

        public BillsMonsterDbContext(DbContextOptions<BillsMonsterDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillsMonsterDbContext).Assembly);
        }
    }
}
