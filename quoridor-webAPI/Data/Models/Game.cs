using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class Game
    {

    private Player[] players;

    private Coordinate[] horizontalWallCoordinates;
    private Coordinate[] verticalWallCoordinates;

       private string validateMove(Move move) {
       if (move.type == "PutWall" ) {
            return validateWallMove(move.coordinate, move.wallType);
       } else {
            return validateStepMove(move.coordinate);
       }
       }

        private string validateWallMove(Coordinate coordinate, string wallType) {
            // walls cannot intersect
            return null;
        }

        private string validateStepMove(Coordinate coordinate) {
            // jump over
            return null;
        }

       public string makeMove(Move move) {
        string moveError = validateMove(move);
         return moveError;
       }


       public bool isOn = false;

       public bool getIsOn() {
            return isOn;
       }

       private int winnerId { get; set; }

       public int getWinnerId(){
        return winnerId;
       }
    }
}
