using System;
using System.Collections.Generic;
using System.Linq;

namespace quoridor_webAPI.Data.Models {
  public class BaseMinimax {

    private static void log(String s) {
      Console.WriteLine(s);
    }

    bool isAllyTurn {
      get;
      set;
    }

    public static List < Move > getPossibleMoves(Player player, Board board, List < Player > players) {
      List < Move > steps = getPossibleStepMoves(player, board, players);

      steps.ForEach(s => log(s.coordinate.x + " " + s.coordinate.y));
      List < Move > walls = getPossibleWallMoves(player, board, players);
      log("walls " + walls.Count);

      return steps;
    }

    private static List < Coordinate > generateWalls() {
      List < Coordinate > walls = new List < Coordinate > ();
      for (int i = 0; i < 8; i++) {
        for (int a = 0; a < 8; a++) {
          walls.Add(new Coordinate(i, a));
        }
      }

      return walls;
    }

    private static List < Move > getPossibleStepMoves(Player player, Board board, List < Player > players) {

      Coordinate c = player.coordinate;
      List < Move > moves = new List < Move > ();

      if (c.y > 0 && !MoveValidator.checkWallsToTheBottom(c, board.getHorizontalWalls())) {
        moves.Add(new Move("Move", null, new Coordinate(c.x, c.y - 1)));
      }

      if (c.y < 8 && !MoveValidator.checkWallsToTheTop(c, board.getHorizontalWalls())) { // check top
        Coordinate c2 = new Coordinate(c.x, c.y + 1);
        Move move = new Move("Move", null, c2);
        moves.Add(move);
      }

      if (c.x > 0 && !MoveValidator.checkWallsToTheLeft(c, board.getVerticalWalls())) {
        moves.Add(new Move("Move", null, new Coordinate(c.x - 1, c.y)));
      }
      if (c.x < 8 && !MoveValidator.checkWallsToTheRight(c, board.getVerticalWalls())) {
        moves.Add(new Move("Move", null, new Coordinate(c.x + 1, c.y)));
      }

      // todo jump
      Player opponent = players[0].Id == player.Id ? players[1] : players[0];
      //  if (opponent.x === )

      return moves;
    }

    private static List < Move > getPossibleWallMoves(Player player, Board board, List < Player > players) {
      List < Move > moves = new List < Move > ();

      if (player.amountOfWalls > 0) {
        List < Coordinate > vW = board.getVerticalWalls();
        List < Coordinate > hW = board.getHorizontalWalls();

        List < Coordinate > vMoveW = generateWalls();
        List < Coordinate > hMoveW = generateWalls();
        log(vW.Count + " wall count " + hW.Count);

        vW.ForEach((w) => {
          vMoveW.Remove(w);
          vMoveW.Remove(new Coordinate(w.x, w.y + 1));
          hMoveW.Remove(new Coordinate(w.x, w.y + 1));
        });

        hW.ForEach((w) => {
          hMoveW.Remove(w);
          hMoveW.Remove(new Coordinate(w.x + 1, w.y));
          vMoveW.Remove(new Coordinate(w.x + 1, w.y));
        });

        vMoveW.ForEach(w => {
          // if touches other wall, check if a star
          //                    if (w.x == 0 || w.y == 0 || w.x == 7 || w.y == 7 || vW.Exists(vW => vW.x == w.x && (vW.y == w.y - 2 || vW.y == w.y + 2))
          //                     || hW.Exists(hW => hW.y == w.y && (hW.x == w.x-2 || hW.x == w.x + 2)) ){
          // todo check a star
          //                     }
          moves.Add(new Move("PutWall", "vertical", w));
        });

        hMoveW.ForEach(w => {
            //                if (w.x == 0 || w.y == 0 || w.x == 7 || w.y == 7 || hW.Exists(hW => hW.y == w.y && (hW.x == w.x - 2 || hW.x == w.x + 2))
            //                                     || vW.Exists(vW => vW.x == w.y && (hW.x == w.x-2 || hW.x == w.x + 2)) ){
            // todo check a star
            //                                     }
            moves.Add(new Move("PutWall", "horizontal", w)));
        }
      }

      return moves;
    }

    int EvaluateTheMove(Move move) // return value of move
    {
      //ehristic algorithm?
      return 0;
    }

    public Move ChooseMove() {
      Coordinate currentCoordinate = new Coordinate(0, 0);
      Move currentMove = new Move("null", "null", currentCoordinate);
      Node root = new Node(currentMove, isAllyTurn); //root of tree
      //root.Insert(Node newMove)
      //fill child nodes with variants
      PriorityQueue < int, Node > possibleMovies = new PriorityQueue < int, Node > (); //store possible moves before alpha or bete pruning
      foreach(Node node in root.childNodes) {
        possibleMovies.Enqueue(EvaluateTheMove(node.currentPosition), node); //set value for every move
      }
      //finish function
      return possibleMovies.Dequeue().currentPosition;
    }
  }
}