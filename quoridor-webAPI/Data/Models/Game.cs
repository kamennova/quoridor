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

        public List < Player > players;

        private bool isOn = false;
        private int currentTurn = 0;

        public Board board = new Board();

        private string validateMove(Move move) {
            if (move.type == "PutWall") {
                return validateWallMove(move.coordinate, move.wallType);
            } else {
                return validateStepMove(move.coordinate);
            }
        }

        private string validateWallMove(Coordinate c, string wallType) {
            if (c.x < 0 || c.x > 7 || c.y < 0 || c.y > 7) {
                return "Over board";
            }

            // intersection
            if (wallType == "horizontal") {
                if (board.getHorizontalWalls().Exists(w => w.y == c.y && (w.x == c.x || w.x -1 == c.x )) || // todo contains
                board.getVerticalWalls().Exists(w => w.y == c.y && w.x == c.x)) {
                    return "Walls intersect";
                }
            } else if (board.getHorizontalWalls().Exists(w => w.y == c.y && (w.x == c.x || w.x -1 == c.x )) || //todo for vertical
                                   board.getVerticalWalls().Exists(w => w.y == c.y && w.x == c.x)) {
                                       return "Walls intersect";
                                   }

            if(doesWallBlockPassing()) {
                return "Wall blocks passing";
            }

            return null;
        }

        private bool doesWallBlockPassing(){
            return false;
        }

        private Player getOpponent() {
            return players[currentTurn == 0 ? 1 : 0];
        }

        private string checkPlayerCollision(Coordinate c) {
            if (c.x == getOpponent().coordinate.x && c.y == getOpponent().coordinate.y) {
                return "Players collide";
            } else if (c.x == players[currentTurn].coordinate.x && c.y == players[currentTurn].coordinate.y) {
                return "Player stands still";
            }

            return null;
        }

        private string validateStepMove(Coordinate c) {
            Coordinate opponentC = getOpponent().coordinate;
            Coordinate currentC = players[currentTurn].coordinate;

            if (c.x < 0 || c.x > 8 || c.y < 0 || c.y > 8) {
                return "Move over board";
            }

            if (Math.Abs(c.x - currentC.x) > 2 || Math.Abs(c.y - currentC.y) > 2) {
                return "Move is too long";
            }

            if (checkPlayerCollision(c) != null) {
                return checkPlayerCollision(c); // todo reformat
            }

            if (c.x != currentC.x && c.y != currentC.y) {
                return "Diagonal moves not allowed";
            }

            if (c.x == currentC.x) {
                Console.WriteLine("vertical move");

                if (Math.Abs(c.y - currentC.y) == 1) { // step
                    if (currentC.y - c.y == 1) { // step up
                        if (MoveValidator.checkWallsToTheTop(currentC, board.getHorizontalWalls())) {
                            return "Cannot move across wall";
                        }
                    } else if (MoveValidator.checkWallsToTheBottom(currentC, board.getHorizontalWalls())) {
                        return "Cannot move across wall";
                    }
                } else {
                    if (currentC.x != opponentC.x || Math.Abs(currentC.y - opponentC.y) != 1) {
                        return "Cannot jump because opponent is not near";
                    }

                    if (currentC.y - opponentC.y > 0) { // jump to bottom
                        if (MoveValidator.checkWallsToTheBottom(opponentC,  board.getHorizontalWalls())) {
                            return WALL_ERR;
                        }
                    } else if (MoveValidator.checkWallsToTheTop(opponentC, board.getHorizontalWalls())) {
                        return WALL_ERR;
                    }
                }
            } else {
                Console.WriteLine("horizontal move");

                if (Math.Abs(c.x - currentC.x) == 1) { // step
                    if (currentC.x - c.x > 0) { // step left
                        if (MoveValidator.checkWallsToTheLeft(currentC, board.getVerticalWalls())) {
                            return "Cannot move across wall";
                        }
                    } else if (MoveValidator.checkWallsToTheRight(currentC, board.getVerticalWalls())) {
                        return "Cannot move across wall";
                    }
                } else { // jump over
                    if (currentC.y != opponentC.y || Math.Abs(currentC.x - opponentC.x) != 1) {
                        return "Cannot jump because opponent is not near";
                    }

                    if (currentC.x - opponentC.x > 0) { // jump to left
                        if (MoveValidator.checkWallsToTheLeft(opponentC, board.getVerticalWalls())) {
                            return WALL_ERR;
                        }
                    } else if (MoveValidator.checkWallsToTheRight(opponentC, board.getVerticalWalls())) {
                        return WALL_ERR;
                    }
                }
            }

            return null;
        }

        private void doMakeMove(Move move) {
            if (move.type == "PutWall") {
                this.players[currentTurn].amountOfWalls--;
                if (move.wallType == "horizontal") {
                    this.board.getHorizontalWalls().Add(move.coordinate);
                } else {
                    this.board.getVerticalWalls().Add(move.coordinate);
                }
            } else {
                this.players[currentTurn].coordinate = move.coordinate;
            }
        }

        private bool CheckIsGameOver() {
            return false;
        }

        public string makeMove(Move move) {
            Console.WriteLine("turn " + currentTurn);
            log(players[currentTurn].coordinate.x + " " + players[currentTurn].coordinate.y + " , moved to " + move.coordinate.x + " " + move.coordinate.y);
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