using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Player_API.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Player> Players { get; set;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
