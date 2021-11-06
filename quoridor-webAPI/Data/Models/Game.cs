using System;
using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models {
  public class Game {
    private string WALL_ERR = "Cannot move across wall";

    public Game() {}

    private bool isOn = false;
    private int currentTurn = 0;
    public bool doLog = true;
    public int moveCounter = 0;
    public GameState state = new GameState();

    public int getWinnerId() {
      return currentTurn;
    }

    private string validateMove(Move move) {
      if (move.type == "PutWall") {
        return validateWallMove(move, state.getPlayer(currentTurn));
      } else {
        return validateStepMove(move.coordinate);
      }
    }

    private string validateWallMove(Move move, PlayerState playerState) {
      if (playerState.amountOfWalls == 0) {
        return "No more walls";
      }

      Coordinate c = move.coordinate;
      if (c.x < 0 || c.x > 7 || c.y < 0 || c.y > 7) {
        return "Over state";
      }

      // intersection
      if (move.wallType == "horizontal") {
        if (state.getHorizontalWalls().Exists(w => w.y == c.y && (w.x == c.x || w.x - 1 == c.x)) || // todo contains
          state.getVerticalWalls().Exists(w => w.y == c.y && w.x == c.x)) {
          return "Walls intersect";
        }
      } else if (state.getVerticalWalls().Exists(w => w.x == c.x && (w.y == c.y || w.y - 1 == c.y)) ||
        state.getHorizontalWalls().Exists(w => w.y == c.y && w.x == c.x)) {
        return "Walls intersect";
      }

      if (doesWallBlockPassing()) {
        return "Wall blocks passing";
      }

      return null;
    }

    private bool doesWallBlockPassing() {
      return false;
    }

    private string checkPlayerCollision(Coordinate c) {
      if (c == state.getOpponent(currentTurn).coordinate) {
        return "Players collide";
      } else if (c == state.getPlayer(currentTurn).coordinate) {
        return "Player stands still";
      }

      return null;
    }

    private string validateStepMove(Coordinate c) {
      Coordinate opponentC = state.getOpponent(currentTurn).coordinate;
      Coordinate currentC = state.getPlayer(currentTurn).coordinate;
      if (c.x < 0 || c.x > 8 || c.y < 0 || c.y > 8) {
        return "Move over board";
      }

      if (Math.Abs(c.x - currentC.x) > 2 || Math.Abs(c.y - currentC.y) > 2) {
        return "Move is too long";
      }

      if (checkPlayerCollision(c) != null) {
        return checkPlayerCollision(c); // todo reformat
      }

      if (c.x != currentC.x && c.y != currentC.y) { // todo
       if(Math.Abs(opponentC.x - currentC.x) +  Math.Abs(opponentC.y - currentC.y) != 1) //opponent near to player?
                      {
                          return "Cannot jump because opponent is not near";
                      }
                      if(Math.Abs(opponentC.x - c.x) +  Math.Abs(opponentC.y - c.y) != 1) //opponent near endpoint?
                      {
                          return "Cannot jump because opponent is not near";
                      }
      }

      if (Math.Abs(currentC.y - c.y) == 2) { // y only jump
       if (currentC.x != opponentC.x || Math.Abs(currentC.y - opponentC.y) != 1) {
                  return "Cannot jump because opponent is not near";
                }

                if (currentC.y - opponentC.y > 0) { // jump to bottom
                  if (MoveValidator.checkWallsToTheBottom(opponentC, state.getHorizontalWalls())) {
                    return WALL_ERR;
                  }
                } else if (MoveValidator.checkWallsToTheTop(opponentC, state.getHorizontalWalls())) {
                  return WALL_ERR;
                }
      } else if (Math.Abs(currentC.x - c.x) == 2) {
                if (currentC.y != opponentC.y || Math.Abs(currentC.x - opponentC.x) != 1) {
                  return "Cannot jump because opponent is not near";
                }

                if (currentC.x - opponentC.x > 0) { // jump to left
                  if (MoveValidator.checkWallsToTheLeft(opponentC, state.getVerticalWalls())) {
                    return WALL_ERR;
                  }
                } else if (MoveValidator.checkWallsToTheRight(opponentC, state.getVerticalWalls())) {
                  return WALL_ERR;
                }
      } else if (c.x == currentC.x) {
        if (Math.Abs(c.y - currentC.y) == 1) { // step
          if (currentC.y - c.y == 1) { // step down
            if (MoveValidator.checkWallsToTheBottom(currentC, state.getHorizontalWalls())) {
              return "Cannot move across wall at the bottom";
            }
          } else if (MoveValidator.checkWallsToTheTop(currentC, state.getHorizontalWalls())) {
            return "Cannot move across wall at the top";
          }
        }
      } else if (c.y == currentC.y) {
        if (Math.Abs(c.x - currentC.x) == 1) { // step
          if (currentC.x - c.x > 0) { // step left
            if (MoveValidator.checkWallsToTheLeft(currentC, state.getVerticalWalls())) {
              return "Cannot move across wall on the left";
            }
          } else if (MoveValidator.checkWallsToTheRight(currentC, state.getVerticalWalls())) {
            return "Cannot move across wall on the right";
          }
        }
      }

      return null;
    }

    private bool CheckIsGameOver() {
      return state.whiteState.coordinate.y == 8 || state.blackState.coordinate.y == 0;
    }

    public string makeMove(Move move) {
      //            log(players[currentTurn].coordinate.x + " " + players[currentTurn].coordinate.y + " , moved to " + move.coordinate.x + " " + move.coordinate.y);
      if (!isOn) {
        return "Game not started";
      }

      string moveError = validateMove(move);

      if (moveError != null) {
        return moveError;
      }

      state.applyMove(move, currentTurn);
      if (CheckIsGameOver()) {
        isOn = false;
        winnerId = currentTurn;
      } else {
        currentTurn = currentTurn == 0 ? 1 : 0;
      }
      moveCounter++;

      return null;
    }

    private void log(String s) {
      if (doLog) {Console.WriteLine(s);}
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