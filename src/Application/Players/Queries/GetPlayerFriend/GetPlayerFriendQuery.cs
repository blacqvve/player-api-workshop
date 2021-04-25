using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Player_API.Application.Common.Exceptions;
using Player_API.Application.Common.Interfaces;
using Player_API.Application.Players.Queries.GetPlayerFriends;

namespace Player_API.Application.Players.Queries.GetPlayerFriend
{
    public class GetPlayerFriendQuery : IRequest<FriendDto>
    {
        public Guid PlayerId { get; set; }
        public Guid FriendId { get; set; }
    }

    public class GetPlayerFriendsQueryHandler : IRequestHandler<GetPlayerFriendQuery, FriendDto>
    {
        private IApplicationDbContext _context;
        private IMapper _mapper;

        public GetPlayerFriendsQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            
            _context = context;
            _mapper = mapper;
        }

        public async Task<FriendDto> Handle(GetPlayerFriendQuery request, CancellationToken cancellationToken)
        {
            var player = await _context.Players.Include(x => x.Friends)
                .FirstOrDefaultAsync(x => x.PlayerId == request.PlayerId, cancellationToken: cancellationToken);

            var friend = await _context.Players.FindAsync(request.FriendId);

            if (player == null)
                throw new NotFoundException(nameof(Player), request.PlayerId);

            if (friend == null)
                throw new NotFoundException(nameof(player), request.FriendId);

            if (!player.Friends.Contains(friend))
                throw new NotFoundException("Player does not have a friend with this id", request.FriendId);

            var mapper = new Mapper(_mapper.ConfigurationProvider);
            return mapper.Map<Player,FriendDto>(friend);
        }
    }
}