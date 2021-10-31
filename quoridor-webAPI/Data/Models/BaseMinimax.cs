using System;
using System.Collections.Generic;
using System.Linq;

namespace quoridor_webAPI.Data.Models {
  public class BaseMinimax {

    private static void log(String s) {
      Console.WriteLine(s);
    }

<<<<<<< HEAD
    public static Dictionary < Move, int > getPossibleMoves(GameState state, int turn) {
      Dictionary < Move, int > steps = getPossibleStepMoves(state, turn);
=======
    bool isAllyTurn {
      get;
      set;
    }

    public static PriorityQueue < Move, int > getPossibleMoves(Player player, Board board, List < Player > players) {
      PriorityQueue < Move, int > steps = getPossibleStepMoves(player, board, players);
>>>>>>> 23318f41d1f404064ad39516ca4f13044e081523

      foreach(var s in steps) {
        log(s.Key.coordinate.x + " " + s.Key.coordinate.y);
      }

<<<<<<< HEAD
      Dictionary < Move, int > walls = getPossibleWallMoves(state, turn);
=======
      PriorityQueue < Move, int > walls = getPossibleWallMoves(player, board, players);
>>>>>>> 23318f41d1f404064ad39516ca4f13044e081523
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

    private static Dictionary < Move, int > getPossibleStepMoves(GameState state, int turn) {
      PlayerState player = state.getPlayer(turn);

//    private static PriorityQueue < Move, int > getPossibleStepMoves(Player player, Board board, List < Player > players) {

      Coordinate c = player.coordinate;
      PriorityQueue < Move, int > moves = new PriorityQueue < Move, int > ();


      if (c.y > 0 && !MoveValidator.checkWallsToTheBottom(c, state.getHorizontalWalls())) {
        moves.Add(new Move("Move", null, new Coordinate(c.x, c.y - 1)), 0);
      }

      if (c.y < 8 && !MoveValidator.checkWallsToTheTop(c, state.getHorizontalWalls())) { // check top
        moves.Add(new Move("Move", null, new Coordinate(c.x, c.y + 1)), 0);
      }

      if (c.x > 0 && !MoveValidator.checkWallsToTheLeft(c, state.getVerticalWalls())) {
        moves.Add(new Move("Move", null, new Coordinate(c.x - 1, c.y)), 0);
      }
      if (c.x < 8 && !MoveValidator.checkWallsToTheRight(c, state.getVerticalWalls())) {
        moves.Add(new Move("Move", null, new Coordinate(c.x + 1, c.y)), 0);
      }

      Coordinate opponentC =  state.getOpponent(turn).coordinate;

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

    private static int evaluateWallMove(GameState state, int turn) {
      PlayerState opponent = state.getOpponent(turn);
      PlayerState player = state.getPlayer(turn);

      int playerGoal = player.color == "white" ? 8 : 0;
      int opponentGoal = opponent.color == "white" ? 8 : 0;

      int distancePlayer = AStar.search(state, player.coordinate, playerGoal);

      if (distancePlayer < 0) {
        return -1;
      }

      int distanceOpponent = AStar.search(state, opponent.coordinate, opponentGoal);

      if (distanceOpponent < 0) {
        return -1;
      }

      return distanceOpponent - distancePlayer;
    }

    private static Dictionary < Move, int > getPossibleWallMoves(GameState state, int turn) {
      Dictionary < Move, int > moves = new Dictionary < Move, int > ();

//    private static PriorityQueue < Move, int > getPossibleWallMoves(Player player, Board board, List < Player > players) {
//      PriorityQueue < Move, int > moves = new PriorityQueue < Move, int > ();

      if (state.getPlayer(turn).amountOfWalls > 0) {
        List < Coordinate > vW = state.getVerticalWalls();
        List < Coordinate > hW = state.getHorizontalWalls();

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
            hW.Exists(hOld => hOld.y == w.y && (hOld.x == w.x - 2 || hOld.x == w.x + 2))) { // todo 2 at once
            rate = evaluateWallMove(state, turn);
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
            rate = evaluateWallMove(state, turn);
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

    int EvaluateMove(Move move) // return value of move
    {
      if (move.type == "PutWall") {

      } else {

      }
      //ehristic algorithm?
      return 0;
    }

    private static void buildTree(Node node, GameState state, int turn, int depth) {
      if (depth == 0) {
        return;
      }

      Dictionary < Move, int > possibleMoves = getPossibleMoves(state, turn);
      foreach(var move in possibleMoves) {
        Node child = new Node(move.Key, move.Value);
        node.Insert(child);
        buildTree(child, state.applyMoveToNew(move.Key, turn), turn == 0 ? 1 : 0, depth - 1);
      }
    }

    private static Node selectNode(Node node, bool isMe) {
      if (node.children.Count > 0) {
        Node max = node.children[0];
        node.children.ForEach(child => {
          Node last = selectNode(child, !isMe);
          int rate = isMe ? child.rate : -child.rate;
          if (rate > max.rate) {
            max = child;
          }
        });

        node.rate = max.rate;
      }

      return node;
    }

    public static Move ChooseMove(GameState state, bool isWhite) {
      int turn = isWhite ? 0 : 1;
      Move zeroMove = new Move("null", "null", new Coordinate(0, 0));
      Node root = new Node(zeroMove, 0);
      buildTree(root, state, turn, 5);
      Node best = selectNode(root, true);

      return best.move;
    }
  }
}