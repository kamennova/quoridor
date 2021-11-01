using System;
using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models {

  public class GameCLI {
    private Game game;
    private static bool doLog = true;
    BotPlayer bot = new BotPlayer(0);
    UserPlayer player = new UserPlayer(1);

    private Coordinate getCoordinate(string input) {
      char col = input[0];
      int row = Int32.Parse(input[1].ToString());

      if ((int) col >= (int)
        'S') { // wall move todo check vertical for 8
        return new Coordinate((int) col - (int)
          'S', 8 - row);
      } else {
        return new Coordinate((int) col - (int)
          'A', 9 - row);
      }
    }

    private string coordToOut(Coordinate c, bool isWall) {
        if (isWall) {
            return ((char) ((int) 'S' + c.x)) + ("" + (8-c.y));
        }

        return ((char) ((int) 'A' + c.x)) + ("" + (9-c.y));
    }

    private static void log(String s) {
      if (doLog) {Console.WriteLine(s);}
    }

    private Move getMove(string[] input) {
      Coordinate c = getCoordinate(input[1]);
      log(c.x + " " + c.y);

      if (input[0] == "move" || input[0] == "jump") {
        return new Move("Step", null, c);
      } else if (input[0] == "wall") {
        string wallType = input[1][2] == 'h' ? "horizontal" : "vertical";
        return new Move("PutWall", wallType, c);
      }

      return null;
    }

    public void executeCommand(string[] input) {
      string command = input[0];

      if (command == "move" || command == "wall" || command == "jump") {
        Move move = getMove(input);
        string error = game.makeMove(move);
        if (error != null) {Console.WriteLine(error);}
      }
    }

    private string moveToOutput(Move move, Coordinate prev) {
        Coordinate c = move.coordinate;
        string cStr = coordToOut(move.coordinate, move.type == "PutWall");
        string type;

        if (move.type == "PutWall") {
            type = "wall";
        } else if (c.x != prev.x && c.y != prev.y || Math.Abs(c.x - prev.x) == 2 || Math.Abs(c.y - prev.y) == 2) {
            type = "jump";
        } else {
            type = "move";
        }

        return type + " " + cStr;
    }

    private void move() {
       Coordinate prev = (bot.isWhite ? game.state.whiteState : game.state.blackState).coordinate;
       Move botMove = bot.getMove(game.state);
       game.makeMove(botMove);
       Console.WriteLine(moveToOutput(botMove, prev));
    }

    public void run(string color) {
      string input;
      bot.isWhite = color == "White";
      this.game = new Game();
      game.start();

      if (bot.isWhite) {
        move();
      }

      do {
        input = Console.ReadLine();
        executeCommand(input.Split(" "));

        if (!game.getIsOn() && game.winnerId != null) {
          Console.WriteLine(game.winnerId);
          log("Game over, winner: " + game.winnerId);
        }

        if (bot.isWhite && game.getTurn() == 0 || !bot.isWhite && game.getTurn() == 1) { // todo
          move();

          if (!game.getIsOn() && game.winnerId != null) {
            Console.WriteLine(game.winnerId);
            log("Game over, winner: " + game.winnerId);
          }
        }
      } while (input != "exit");
    }

    public static void Main(String[] args) {
      GameCLI gameCli = new GameCLI();

      if (args[1] == "no-log") {
        doLog = false;
        BaseMinimax.doLog = false;
      }

      gameCli.run(args[0]);
    }
  }
}
