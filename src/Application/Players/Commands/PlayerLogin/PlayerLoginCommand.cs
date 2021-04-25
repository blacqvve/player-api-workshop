using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Player_API.Application.Common.Exceptions;
using Player_API.Application.Common.Interfaces;

namespace Application.Players.Commands.PlayerLogin
{
    public class PlayerLoginCommand : IRequest<DateTime>
    {
        public Guid PlayerId { get; set; }
    }

    public class PlayerLoginCommandHandler : IRequestHandler<PlayerLoginCommand,DateTime>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;


        public PlayerLoginCommandHandler(IApplicationDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }
        public async Task<DateTime> Handle(PlayerLoginCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Players.FirstOrDefault(x => x.PlayerId == request.PlayerId);

            if (entity == null)
                throw new NotFoundException(nameof(Player), request.PlayerId);

            entity.LastLogin = _dateTime.Now;

            _context.Players.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.LastLogin;
        }
    }
}