using BillsMonster.Application.Interfaces;
using BillsMonster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BillsMonster.Persistence
{
    public class BillsMonsterDbContext : IBillsMonsterDbContext // DbContext, 
    {
        //public DbSet<Bill> Bills { get; set; }
        //public DbSet<Group> Groups { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Reminder> Reminders { get; set;}

        //public BillsMonsterDbContext(DbContextOptions<BillsMonsterDbContext> options): base(options)
        //{
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillsMonsterDbContext).Assembly);
        //}

        public IMongoCollection<Bill> Bills { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IMongoCollection<Group> Groups { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IMongoCollection<User> Users { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IMongoCollection<Reminder> Reminders { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
