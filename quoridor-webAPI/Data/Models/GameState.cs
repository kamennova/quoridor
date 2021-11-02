using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models {
  public class GameState {
    public List < Coordinate > horizontalWalls = new List < Coordinate > ();
    public List < Coordinate > verticalWalls = new List < Coordinate > ();
    public PlayerState blackState = new PlayerState(new Coordinate(4, 8), "black");
    public PlayerState whiteState = new PlayerState(new Coordinate(4, 0), "white");

    public PlayerState getPlayer(int turn) {
      return turn == 0 ? whiteState : blackState;
    }

    public PlayerState getOpponent(int turn) {
      return turn == 1 ? whiteState : blackState;
    }

    public List < Coordinate > getVerticalWalls() {
      return verticalWalls;
    }

    public List < Coordinate > getHorizontalWalls() {
      return horizontalWalls;
    }

    private void applyMoveToState(GameState state, Move move, int turn) {
      PlayerState player = state.getPlayer(turn);

      if (move.type == "PutWall") {
        player.amountOfWalls--;
        if (move.wallType == "vertical") {
          state.verticalWalls.Add(move.coordinate);
        } else {
          state.horizontalWalls.Add(move.coordinate);
        }
      } else {
        player.coordinate = move.coordinate;
      }
    }

    public void applyMove(Move move, int turn) {
      applyMoveToState(this, move, turn);
    }

    public GameState applyMoveToNew(Move move, int turn) {
      GameState state = this.copy();
      applyMoveToState(state, move, turn);
      return state;
    }

    public GameState copy() {
      GameState state = new GameState();

      state.horizontalWalls = copyWalls(horizontalWalls); // todo copies
      state.verticalWalls = copyWalls(verticalWalls);
      state.whiteState = whiteState.copy();
      state.blackState = blackState.copy();

      return state;
    }

    private List<Coordinate> copyWalls(List<Coordinate> walls) {
        List<Coordinate> c = new List<Coordinate> ();

        walls.ForEach(w => c.Add(new Coordinate(w.x, w.y)));

        return c;
    }
  }
}
