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

        public Move getMove(GameState state) {
            return BaseMinimax.ChooseMove(state, isWhite);
        }
    }
}