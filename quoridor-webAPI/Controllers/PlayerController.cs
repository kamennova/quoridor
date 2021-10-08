using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using quoridor_webAPI.Data.Services;
using quoridor_webAPI.Data.ViewModels;

namespace quoridor_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        public PlayerService _playerService;

        public PlayerController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("get-valid-position")]
        public IActionResult ValidPosition()
        {
            var Coordinates = _playerService.GetValidPosition();
            return Ok(Coordinates);
        }

        [HttpPost("get-player-by-id")]
        public IActionResult AddPlayer([FromBody] PlayerVM player)
        {
            _playerService.AddPlayer(player);
            return Ok();
        }

        [HttpPut("update-player")]
        public IActionResult UpdatePlayer([FromBody] PlayerVM player)
        {
            _playerService.UpdatePlayer(player);
            return Ok();
        }
    }
}
