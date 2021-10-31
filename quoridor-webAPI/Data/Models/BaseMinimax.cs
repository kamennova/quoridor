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

    public static PriorityQueue < Move, int > getPossibleMoves(Player player, Board board, List < Player > players) {
      PriorityQueue < Move, int > steps = getPossibleStepMoves(player, board, players);

      foreach(var s in steps) {
        log(s.Key.coordinate.x + " " + s.Key.coordinate.y);
      }

      PriorityQueue < Move, int > walls = getPossibleWallMoves(player, board, players);
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

    private static PriorityQueue < Move, int > getPossibleStepMoves(Player player, Board board, List < Player > players) {

      Coordinate c = player.coordinate;
      PriorityQueue < Move, int > moves = new PriorityQueue < Move, int > ();

      if (c.y > 0 && !MoveValidator.checkWallsToTheBottom(c, board.getHorizontalWalls())) {
        moves.Enqueue(new Move("Move", null, new Coordinate(c.x, c.y - 1)), 0);
      }

      if (c.y < 8 && !MoveValidator.checkWallsToTheTop(c, board.getHorizontalWalls())) { // check top
        moves.Enqueue(new Move("Move", null, new Coordinate(c.x, c.y + 1)), 0);
      }

      if (c.x > 0 && !MoveValidator.checkWallsToTheLeft(c, board.getVerticalWalls())) {
        moves.Enqueue(new Move("Move", null, new Coordinate(c.x - 1, c.y)), 0);
      }
      if (c.x < 8 && !MoveValidator.checkWallsToTheRight(c, board.getVerticalWalls())) {
        moves.Enqueue(new Move("Move", null, new Coordinate(c.x + 1, c.y)), 0);
      }

      // todo jump
      Player opponent = players[0].Id == player.Id ? players[1] : players[0];
      Coordinate opponentC = opponent.coordinate;

      if(Math.Abs(opponentC.x - c.x) +  Math.Abs(opponentC.y - c.y) == 1) //opponent near?
      {
        Coordinate vectorToOpponent = new Coordinate(opponentC.x-c.x,opponentC.y-c.y);
        if(vectorToOpponent.x == 0 && vectorToOpponent.y == 1){ //opponet on top
          if(!checkWallsToTheTop(opponentC, board.getHorizontalWalls())){//jump ?
              //moves.Enqueue(new move()) // todo add correctly jump (+0, +2)
          }
          else{//try diagonal jump
            if(!checkWallsToTheLeft(opponentC, board.getVerticalWalls()))
            {
              // todo add correctly jump (+1, +1)
            }
            if(!checkWallsToTheRight(opponentC, board.getVerticalWalls()))
            {
              // todo add correctly jump (-1, +1)
            }
          }
        }
        if(vectorToOpponent.x == 0 && vectorToOpponent.y == -1){//opponet from below
          if(!checkWallsToTheBottom(opponentC, board.getHorizontalWalls())){//jump ?
              //moves.Enqueue(new move()) // todo add correctly jump (+0, -2)
          }
          else{//try diagonal jump
            if(!checkWallsToTheLeft(opponentC, board.getVerticalWalls()))
            {
              // todo add correctly jump (+1, -1)
            }
            if(!checkWallsToTheRight(opponentC, board.getVerticalWalls()))
            {
              // todo add correctly jump (-1, -1)
            }
          }
        }
        if(vectorToOpponent.x == 1 && vectorToOpponent.y == 0){//opponet on the right
          if(!checkWallsToTheRight(opponentC, board.getVerticalWalls())){//jump ?
              //moves.Enqueue(new move()) // todo add correctly jump (+2, 0)
          }
          else{//try diagonal jump
            if(!checkWallsToTheTop(opponentC, board.getHorizontalWalls()))
            {
              // todo add correctly jump (+1, +1)
            }
            if(!checkWallsToTheBottom(opponentC, board.getHorizontalWalls()))
            {
              // todo add correctly jump (+1, -1)
            }
          }
        }
        if(vectorToOpponent.x == -1 && vectorToOpponent.y == 0){//opponet on the left
          if(!checkWallsToTheLeft(opponentC, board.getVerticalWalls())){//jump ?
              //moves.Enqueue(new move()) // todo add correctly jump (-2, 0)
          }
          else{//try diagonal jump
            if(!checkWallsToTheTop(opponentC, board.getHorizontalWalls()))
            {
              // todo add correctly jump (-1, +1)
            }
            if(!checkWallsToTheBottom(opponentC, board.getHorizontalWalls()))
            {
              // todo add correctly jump (-1, -1)
            }
          }
        }
      }

      return moves;
    }

    private static int evaluateWallMove(Board board, List < Player > players, int playerId) {
      Player opponent = players[0].Id == playerId ? players[1] : players[0];
      Player player = players[1].Id == playerId ? players[1] : players[0];

      int distancePlayer = AStar.search(board, player.coordinate, 0); // todo goal

      if (distancePlayer < 0) {
        return -1;
      }

      int distanceOpponent = AStar.search(board, opponent.coordinate, 0); // todo goal

      if (distanceOpponent < 0) {
        return -1;
      }

      return distanceOpponent - distancePlayer;
    }

    private static PriorityQueue < Move, int > getPossibleWallMoves(Player player, Board board, List < Player > players) {
      PriorityQueue < Move, int > moves = new PriorityQueue < Move, int > ();

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
          int rate = 0;

          // if touches other wall, check if can be passable with a star
          if (w.x == 0 || w.y == 0 || w.x == 7 || w.y == 7 || vW.Exists(vOld => vOld.x == w.x && (vOld.y == w.y - 2 || vOld.y == w.y + 2)) ||
            hW.Exists(hOld => hOld.y == w.y && (hOld.x == w.x - 2 || hOld.x == w.x + 2))) {
            rate = evaluateWallMove(board, players, player.Id);
            if (rate < 0) {
              return;
            }
          } else {
            // todo rate quick?
          }
          moves.Enqueue(new Move("PutWall", "vertical", w), rate);
        });

        hMoveW.ForEach(w => {
          int rate = 0;
          if (w.x == 0 || w.y == 0 || w.x == 7 || w.y == 7 || hW.Exists(hOld => hOld.y == w.y && (hOld.x == w.x - 2 || hOld.x == w.x + 2)) ||
            vW.Exists(vOld => vOld.x == w.y && (vOld.x == w.x - 2 || vOld.x == w.x + 2))) {
            rate = evaluateWallMove(board, players, player.Id);
            if (rate < 0) {
              return;
            }
          } else {
            // todo rate quick
          }
          moves.Enqueue(new Move("PutWall", "horizontal", w), rate);
        });
      }

      return moves;
    }

    int EvaluateTheMove(Move move) // return value of move
    {
      //ehristic algorithm?
      return 0;
    }

    private void minimaxStep() {

    }

    public static Move ChooseMove(Board board, Player player, List < Player > players) {
      Coordinate currentCoordinate = new Coordinate(0, 0);
      //      Move currentMove = new Move("null", "null", currentCoordinate);
      //      Node root = new Node(currentMove, isAllyTurn); //root of tree
      //root.Insert(Node newMove)
      //fill child nodes with variants
      //      PriorityQueue < int, Node > possibleMovies = new PriorityQueue < int, Node > (); //store possible moves before alpha or bete pruning
      //      foreach(Node node in root.childNodes) {
      //        possibleMovies.Enqueue(EvaluateTheMove(node.currentPosition), node); //set value for every move
      //      }
      //finish function
      //      return possibleMovies.Dequeue().currentPosition;
      return new Move("null", "dfs", currentCoordinate);
    }
  }
}