using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class BotPlayer : Player
    {

        public BotPlayer(int id) : base(id) {
        }

        public Move makeMove(Board board, List<Player> players) {
                return generateRandomMove(board, players);
        }

        private List<Coordinate> getPossibleSteps(Board board, List<Player> players){
            List<Coordinate> coords = new List<Coordinate>();
            if (this.Coordinate.y < 8 &&
                !board.getHorizontalWalls().Contains(new Coordinate(this.Coordinate.x, this.Coordinate.y)) &&
                !board.getHorizontalWalls().Contains(new Coordinate(this.Coordinate.x-1, this.Coordinate.y))
                ) { // check top
                coords.Add(new Coordinate(this.Coordinate.x, this.Coordinate.y + 1));
            }

            if (this.Coordinate.y > 0 &&
            !board.getHorizontalWalls().Contains(new Coordinate(this.Coordinate.x, this.Coordinate.y-1)) &&
                            !board.getHorizontalWalls().Contains(new Coordinate(this.Coordinate.x-1, this.Coordinate.y-1))
                            ){
                coords.Add(new Coordinate(this.Coordinate.x, this.Coordinate.y-1));
                            }

            return coords;
        }

        private Move generateRandomMove(Board board, List<Player> players){
            List<Coordinate> possible = getPossibleSteps(board, players);

            return new Move("Step", null, possible[0]); // todo random
        }
    }
}