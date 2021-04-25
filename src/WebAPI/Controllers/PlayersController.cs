using Application.Players.Commands.CreatePlayer;
using Application.Players.Commands.PlayerLogin;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class PlayersController : ApiControllerBase
    {
        [HttpPost("CreatePlayer")]
        public async Task<ActionResult<Guid>> CreatePlayer(CreatePlayerCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("PlayerLogin")]
        public async Task<ActionResult> PlayerLogin([FromBody] Guid id)
        {
            
            await Mediator.Send(new PlayerLoginCommand{PlayerId = id});

            return Ok();
        }
    }
}