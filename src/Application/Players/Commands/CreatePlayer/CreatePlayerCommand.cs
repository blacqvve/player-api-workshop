using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Player_API.Application.Common.Interfaces;

namespace Application.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommand : IRequest<Guid>
    {
        
    }
     public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreatePlayerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Player
            {
                PlayerId = Guid.NewGuid(),
                Active = true
            };

            _context.Players.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.PlayerId;
        }
    }
}