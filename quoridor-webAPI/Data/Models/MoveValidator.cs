using System;
using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models {
    public class MoveValidator {

     public static bool checkWallsToTheLeft(Coordinate currentC, List<Coordinate> walls) {
                return walls.Exists(w => (currentC.y == w.y || currentC.y == w.y - 1) && currentC.x == w.x);
            }

           public static bool checkWallsToTheRight(Coordinate currentC, List<Coordinate> walls) {
                return walls.Exists(w => (currentC.y == w.y || currentC.y == w.y - 1) && currentC.x == w.x - 1);
            }

            public static bool checkWallsToTheTop(Coordinate currentC, List<Coordinate> walls) {
                return walls.Exists(w => (currentC.x == w.x || currentC.x == w.x - 1) && currentC.y == w.y);
            }

           public static bool checkWallsToTheBottom(Coordinate currentC, List<Coordinate> walls) {
                return walls.Exists(w => (currentC.x == w.x || currentC.x == w.x - 1) && currentC.y == w.y - 1);
            }

    }
}