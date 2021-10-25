using System;
using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models {
    public class Game {
        private string WALL_ERR = "Cannot move across wall";

        public Game(List < Player > players) {
            this.players = players;
            this.players[0].coordinate = new Coordinate(4, 0);
            this.players[1].coordinate = new Coordinate(4, 8);
        }

        private List < Player > players;

        private bool isOn = false;
        private int currentTurn = 0;

        private Board board = new Board();

        //    ---
        private List < Coordinate > horizontalWallCoordinates;
        private List < Coordinate > verticalWallCoordinates;

        private string validateMove(Move move) {
            if (move.type == "PutWall") {
                return validateWallMove(move.coordinate, move.wallType);
            } else {
                return validateStepMove(move.coordinate);
            }
        }

        private string validateWallMove(Coordinate coordinate, string wallType) {
            // walls cannot intersect
            //horizontal
            if (wallType == "horizontal") {
                for (int i = 0; horizontalWallCoordinates.Count > 0; i++) {
                    if (coordinate.x == horizontalWallCoordinates[i].x || coordinate.y == horizontalWallCoordinates[i].y) {
                        return "incorrect move";
                    }
                }
            }
            //vertical
            if (wallType == "vertical") {
                for (int i = 0; verticalWallCoordinates.Count > 0; i++) {
                    if (coordinate.x == verticalWallCoordinates[i].x || coordinate.y == verticalWallCoordinates[i].y) {
                        return "incorrect move";
                    }
                }
            }
            //passage  chek

            return null;
        }

        private Player getOpponent() { // todo
            return players[1];
        }

        private string checkPlayerCollision(Coordinate c) {
            if (c.x == getOpponent().coordinate.x && c.y == getOpponent().coordinate.y) {
                return "Players collide";
            } else if (c.x == players[currentTurn].coordinate.x && c.y == players[currentTurn].coordinate.y) {
                return "Player stands still";
            }

            return null;
        }

        private bool checkWallsToTheLeft(Coordinate currentC) {
            return board.getVerticalWalls().Exists(w => (currentC.y == w.y || currentC.y == w.y - 1) && currentC.x == w.x);
        }

        private bool checkWallsToTheRight(Coordinate currentC) {
            return board.getVerticalWalls().Exists(w => (currentC.y == w.y || currentC.y == w.y - 1) && currentC.x == w.x - 1);
        }

        private bool checkWallsToTheTop(Coordinate currentC) {
            return board.getHorizontalWalls().Exists(w => (currentC.x == w.x || currentC.x == w.x - 1) && currentC.y == w.y);
        }

        private bool checkWallsToTheBottom(Coordinate currentC) {
            return board.getHorizontalWalls().Exists(w => (currentC.x == w.x || currentC.x == w.x - 1) && currentC.y == w.y - 1);
        }


        private string validateStepMove(Coordinate c) {
            Coordinate opponentC = getOpponent().coordinate;
            Coordinate currentC = players[currentTurn].coordinate;

            if (c.x < 0 || c.x > 8 || c.y < 0 || c.y > 8) {
                return "Move over board";
            }

            if (Math.abs(c.x - currentC.x) > 2 || Math.abs(c.y - currentC.y) > 2) {
                return "Move is too long";
            }

            if (checkPlayerCollision(c) != null) {
                return checkPlayerCollision(c); // todo
            }

            if (c.x != currentC.x && c.y != currentC.y) {
                return "Diagonal moves not allowed";
            }

            if (c.x == currentC.x) {
                Console.WriteLine("vertical move");

                if (Math.Abs(c.y - currentC.y) == 1) { // step
                    if (currentC.y - c.y == 1) { // step up
                        if (!checkWallsToTheTop(currentC)) {
                            return "Cannot move across wall";
                        }
                    } else if (!checkWallsToTheBottom(currentC)) {
                        return "Cannot move across wall";
                    }
                } else if (Math.Abs(c.y - currentC.y) == 2) {
                    if (currentC.x != opponentC.x || Math.Abs(currentC.y - opponentC.y) != 1) {
                        return "Cannot jump because opponent is not near";
                    }

                    if (currentC.y - opponentC.y > 0) { // jump to bottom
                        if (!checkWallsToTheBottom(opponentC)) {
                            return WALL_ERR;
                        }
                    } else if (!checkWallsToTheTop(opponentC)) {
                        return WALL_ERR;
                    }
                }
            } else if (c.y == currentC.y) {
                Console.WriteLine("horizontal move");

                if (Math.Abs(c.x - currentC.x) == 1) { // step
                    if (currentC.x - c.x > 0) { // step left
                        if (!checkWallsToTheLeft(currentC)) {
                            return "Cannot move across wall";
                        }
                    } else if (!checkWallsToTheRight(currentC)) {
                        return "Cannot move across wall";
                    }
                } else if (Math.Abs(c.x - currentC.x) == 2) { // jump over

                    if (currentC.y != opponentC.y || Math.Abs(currentC.x - opponentC.x) != 1) {
                        return "Cannot jump because opponent is not near";
                    }

                    if (currentC.x - opponentC.x > 0) { // jump to left
                        if (!checkWallsToTheLeft(opponentC)) {
                            return WALL_ERR;
                        }
                    } else if (!checkWallsToTheRight(opponentC)) {
                        return WALL_ERR;
                    }

                }
            }

            return null;
        }

        private void doMakeMove(Move move) {

        }

        private Move lastMove;

        private bool CheckIsGameOver() {
            return false;
        }

        //------------------------

        public string makeMove(Move move) {
            Console.WriteLine(currentTurn);
            Console.WriteLine(players[currentTurn].Id);
            log(players[currentTurn].coordinate.y + " " + players[currentTurn].coordinate.x);
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
                log("here");
            } else {
                currentTurn = currentTurn == 0 ? 1 : 0;
            }

            return null;
        }
        private void log(String s) {
            Console.WriteLine(s);
        }
        public void start() {
            winnerId = null;
            isOn = true;
        }

        public Move getLastMove() {
            return lastMove;
        }

        public int getTurn() {
            return currentTurn;
        }

        public bool getIsOn() {
            return isOn;
        }

        public int ? winnerId {
            get;
            set;
        }
    }
}