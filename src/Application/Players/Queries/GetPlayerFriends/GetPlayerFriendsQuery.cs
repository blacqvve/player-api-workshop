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

namespace Player_API.Application.Players.Queries.GetPlayerFriends
{
    public class GetPlayerFriendsQuery : IRequest<List<FriendDto>>
    {
        public Guid PlayerId { get; set; }
    }
    
    
    public class GetPlayerFriendsQueryHandler : IRequestHandler<GetPlayerFriendsQuery,List<FriendDto>>
    {
        private IApplicationDbContext _context;
        
        private IMapper _mapper;

        public GetPlayerFriendsQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FriendDto>> Handle(GetPlayerFriendsQuery request, CancellationToken cancellationToken)
        {
            var player = await _context.Players.Include(x => x.Friends)
                .FirstOrDefaultAsync(x => x.PlayerId == request.PlayerId, cancellationToken: cancellationToken);

            if (player == null)
                throw new NotFoundException(nameof(Player), request.PlayerId);
            
            var mapper = new Mapper(_mapper.ConfigurationProvider);
            return player.Friends.Select(x => mapper.Map<Player, FriendDto>(x)).ToList();

        }
    }
}