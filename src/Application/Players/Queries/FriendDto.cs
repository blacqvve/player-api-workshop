using System;
using Domain.Entities;
using Player_API.Application.Common.Mappings;

namespace Player_API.Application.Players.Queries
{
    public class FriendDto : IMapFrom<Player>
    {
        public Guid PlayerId { get; set; }
        
        public DateTime LastLogin { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public bool Active { get; set; }
    }
}