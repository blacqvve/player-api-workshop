using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Player_API.Application.Common.Exceptions;
using Player_API.Application.Common.Interfaces;

namespace Application.Players.Commands.AddFriend
{
    public class AddFriendCommand : IRequest
    {
        public Guid PlayerId { get; set; }

        public Guid FriendId { get; set; }
    }

    public class AddFriendCommandHandler : IRequestHandler<AddFriendCommand>
    {

        private IApplicationDbContext _context;
        public AddFriendCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            var player = _context.Players.FirstOrDefault(x => x.PlayerId == request.PlayerId);

            var friend = _context.Players.FirstOrDefault(x => x.PlayerId == request.FriendId);

            if (player == null)
                throw new NotFoundException(nameof(Player), request.PlayerId);
            if (friend == null)
                throw new NotFoundException(nameof(Player), request.FriendId);

            player.Friends.Add(friend);

            friend.Friends.Add(player);

            _context.Players.UpdateRange(new Player[]{player,friend});

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
            
        }

    }
}