using Application.Players.Commands.CreatePlayer;
using Application.Players.Commands.PlayerLogin;
using Application.Players.Commands.AddFriend;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.Entities;
using Player_API.Application.Players.Queries;
using Player_API.Application.Players.Queries.GetPlayerFriend;
using Player_API.Application.Players.Queries.GetPlayerFriends;

namespace WebAPI.Controllers
{
    public class PlayersController : ApiControllerBase
    {
        [HttpGet("CreatePlayer")]
        public async Task<ActionResult<Guid>> CreatePlayer()
        {
            return await Mediator.Send(new CreatePlayerCommand());
        }

        [HttpPost("PlayerLogin/{id}")]
        public async Task<ActionResult<DateTime>> PlayerLogin(Guid id)
        {
            return await Mediator.Send(new PlayerLoginCommand{PlayerId = id});
        }
        
        [HttpPost("AddFriend")]

        public async Task<ActionResult> AddFriend(AddFriendCommand request)
        {
            await Mediator.Send(request);

            return Ok();
        }

        [HttpGet("GetPlayerFriends/{id}")]

        public async Task<ActionResult<List<FriendDto>>> GetPlayerFriends([FromQuery] Guid playerId)
        {
            return await Mediator.Send(new GetPlayerFriendsQuery {PlayerId = playerId});
        }
        
        [HttpGet("GetPlayerFriend")]

        public async Task<ActionResult<FriendDto>> GetPlayerFriend([FromBody] GetPlayerFriendQuery request)
        {
            return await Mediator.Send(request);
        }
    }
}