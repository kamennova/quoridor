using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class Player {
        public Player(int id) {
            this.Id = id;
        }

        public int Id { get; set; }

        public Coordinate coordinate;

        public int amountOfWalls = 10;

        public Move makeMove(Board board, List<Player> players){
            return new Move("PutWall", "horizontal", new Coordinate(5, 0));
        }

        public void updateCoordinate(Coordinate coordinate){
            this.coordinate = coordinate;
        }
    }
}
