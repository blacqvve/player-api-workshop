using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Player_API.Application.Common.Interfaces;
using Player_API.Domain.Entities;

namespace Infrastructure.Persistence
{
    public class PlayerDbContext : DbContext,IApplicationDbContext
    {
        private readonly IDateTime _dateTime;

        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Player> Players { get; set; }

        public PlayerDbContext(
            DbContextOptions options,
            IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<IHasCreateDate>())
            {
                if(entity.State == EntityState.Added)
                {
                    entity.Entity.CreateDate = _dateTime.Now;
                }
                
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}