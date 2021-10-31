using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models {
  public class PlayerState {
    public PlayerState(Coordinate c, string color) {
      this.coordinate = c;
      this.color = color;
    }

    public string color;

    public Coordinate coordinate;

    public int amountOfWalls = 10;

    public PlayerState copy() {
      PlayerState state = new PlayerState(coordinate, color);
      state.amountOfWalls = amountOfWalls;
      return state;
    }
  }
}
