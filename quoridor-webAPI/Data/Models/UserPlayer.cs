using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class UserPlayer : Player
    {

        Func<GameState, List<Player>> onMakeMove;
        public UserPlayer(int id
//        , Func<Board, List<Player>> onMakeMove
        ) : base(id) {
//            this.onMakeMove = onMakeMove;
        }

        public Move makeMove(GameState state) {
//                Move move = onMakeMove(board, players);
//                return move;
return new Move("asd", null, new Coordinate(0, 0));
        }

    }
}