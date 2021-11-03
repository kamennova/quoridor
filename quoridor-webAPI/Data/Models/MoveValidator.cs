using System;
using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models
{
    public class MoveValidator
    {

        public static bool checkWallsToTheLeft(Coordinate currentC, List<Coordinate> walls)
        {
            return walls.Exists(w => (currentC.y == w.y || currentC.y == w.y + 1) && currentC.x == w.x + 1);
        }

        public static bool checkWallsToTheRight(Coordinate currentC, List<Coordinate> walls)
        {
            return walls.Exists(w => (currentC.y == w.y || currentC.y == w.y + 1) && currentC.x == w.x);
        }

        public static bool checkWallsToTheTop(Coordinate currentC, List<Coordinate> walls)
        {
            return walls.Exists(w => (currentC.x == w.x || currentC.x == w.x + 1) && currentC.y == w.y);
        }

        public static bool checkWallsToTheBottom(Coordinate currentC, List<Coordinate> walls)
        {
            return walls.Exists(w => (currentC.x == w.x || currentC.x == w.x + 1) && currentC.y == w.y + 1);
        }

        public static bool isOpponentNear(GameState state, Coordinate c, int turn)
        {
            Coordinate c2 = state.getOpponent(turn).coordinate; // ??? todo turn + 1
            return Math.Abs(c2.x - c.x) == 1 && Math.Abs(c2.y - c.y) == 1;
        }

        public static List<Move> getPossibleSimpleStepMoves(Coordinate c, GameState state)
        {
            List<Move> moves = new List<Move>();

            Coordinate bottom = new Coordinate(c.x, c.y - 1);
            if (c.y > 0 && !MoveValidator.checkWallsToTheBottom(c, state.getHorizontalWalls()))
            {
                moves.Add(new Move("Move", null, bottom));
            }

            Coordinate top = new Coordinate(c.x, c.y + 1);
            if (c.y < 8 && !MoveValidator.checkWallsToTheTop(c, state.getHorizontalWalls()))
            { // check top
                moves.Add(new Move("Move", null, top));
            }

            Coordinate left = new Coordinate(c.x - 1, c.y);
            if (c.x > 0 && !MoveValidator.checkWallsToTheLeft(c, state.getVerticalWalls()))
            {
                moves.Add(new Move("Move", null, left));
            }

            Coordinate right = new Coordinate(c.x + 1, c.y);
            if (c.x < 8 && !MoveValidator.checkWallsToTheRight(c, state.getVerticalWalls()))
            {
                moves.Add(new Move("Move", null, right));
            }

            return moves;
        }
    }
}