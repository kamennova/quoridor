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

        private int lastPlayerMovedTurn;
        private Move lastPlayerMove;

        private string gameMode;

        public GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        private Game game;

        [HttpPost("start")]
        public IActionResult StartGame([FromBody] string mode)
        {
            Player player1 = new UserPlayer(0);
            Player player2 = mode == "WithComputer" ? new BotPlayer(1) : new UserPlayer(1);
            Player[] playersArr = {player1, player2};
            List<Player> players = new List<Player>(playersArr);
            game = new Game(players);
            gameMode = mode;

            Coordinate[] startCoords = {new Coordinate(4, 0), new Coordinate(4, 8)};
            return Ok(new GameStart(new List<Coordinate>(startCoords)));
        }

        [HttpGet("get-opponent-move")]
        public IActionResult GetOpponentMove()
        {
            Move move = game.getLastMove();
            return Ok(move);
        }


        [HttpPost("try-move")]
        public IActionResult TryMove([FromBody] Move move)
        {
          string error;
          if (game == null) {
            error = "Game not started";
          } else {
            error = game.makeMove(move);
          }

          MoveValidationResult result;

           if (error != null) {
                result = new MoveValidationResult(false, false);
                result.setMoveError(error);
                return BadRequest(result);
           }

           result = new MoveValidationResult(true, !game.getIsOn());
           if (!game.getIsOn()) {
                result.setWinnerId(game.getWinnerId());
           }

           return Ok(result);
        }
    }
}
