using Player_API.Domain.Common;
using System.Threading.Tasks;

namespace Player_API.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
