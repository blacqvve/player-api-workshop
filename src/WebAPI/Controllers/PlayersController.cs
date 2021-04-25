using Application.Players.Commands.CreatePlayer;
using Application.Players.Commands.PlayerLogin;
using Application.Players.Commands.AddFriend;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<ActionResult> PlayerLogin([FromQuery] Guid id)
        {
            
           var lastLogin =  await Mediator.Send(new PlayerLoginCommand{PlayerId = id});

            return Ok(lastLogin);
        }
        
        [HttpPost("AddFriend")]

        public async Task<ActionResult> AddFriend(AddFriendCommand request)
        {
            await Mediator.Send(request);

            return Ok();
        }
    }
}