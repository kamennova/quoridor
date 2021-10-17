using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class Game
    {

    public Game(List<Player> players) {
        this.players = players;
    }

    private List<Player> players;

    private bool isOn = false;
    private int currentTurn = 0;

    private Board board = new Board();

//    ---

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

        private void doMakeMove(Move move) {

        }

        private Move lastMove;

        private bool CheckIsGameOver(){
                return false;
                }

//------------------------

       public string makeMove(Move move) {
         if (!isOn) {
         return "Game not started";
         }

        string moveError = validateMove(move);

        if (moveError != null) {
            return moveError;
        }

        doMakeMove(move);
        if (CheckIsGameOver()) {
            isOn = false;
            winnerId = currentTurn;
        } else {
            currentTurn = currentTurn == 0 ? 1 : 0;
        }

         return null;
       }

       public void startGame(){

       }


        public Move getLastMove(){
            return lastMove;
        }

        public int getTurn() {
            return currentTurn;
        }

       public bool getIsOn() {
            return isOn;
       }

       private int winnerId { get; set; }

       public int getWinnerId(){
        return winnerId;
       }
    }
}
