using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using quoridor_webAPI.Data.ViewModels;
using quoridor_webAPI.Data.Models;
using quoridor_webAPI.Data.Services;

namespace quoridor_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        public GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }
        private bool isGameOn = false;
        private Game game;

        [HttpPost("try-move")]
        public IActionResult TryMove([FromBody] Move move)
        {

          string error;
          if (isGameOn == false) {
            error = "Game not started";
          } else {
            error = game.validateMove(move);
          }

           if (error != null) {
                MoveValidationResult result = new MoveValidationResult(false, false);
                result.setMoveError(error);
                return BadRequest(result);
           }

           game.makeMove(move);
           MoveValidationResult result = new MoveValidationResult(true, !game.getIsOn());
           if (game.isOver()) {
                result.setWinnerId(game.getWinnerId());
           }

           return Ok(result);
        }
    }
}
