using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class BotPlayer : Player
    {
        public bool isWhite;
        public BotPlayer(int id) : base(id) {
        }

        private Move debuts(Coordinate current) {
            Coordinate next = isWhite ? new Coordinate(current.x, current.y + 1) : new Coordinate(current.x, current.y-1);
            return new Move("Step", null, next);
        }

        public Move getMove(GameState state) {
        int turn = isWhite ? 0 : 1;

        if (state.moveIndex < 4 && state.getHorizontalWalls().Count == 0) {
            return debuts(state.getPlayer(turn).coordinate);
        }

            return BaseMinimax.ChooseMove(state, turn);
        }
    }
}