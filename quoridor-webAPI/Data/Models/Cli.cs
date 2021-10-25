using System;
using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models {

  public class GameCLI {
    private Game game;

    private Coordinate getCoordinate(string input) {
      char col = input[0];
      int row = Int32.Parse(input[1].ToString());

      if ((int) col >= (int)
        'S') { // wall move
        return new Coordinate((int) col - (int)
          'S', 8 - row);
      } else {
        return new Coordinate((int) col - (int)
          'A', 9 - row);
      }
    }

    private void log(String s) {
      Console.WriteLine(s);
    }

    private Move getMove(string[] input) {
      Coordinate c = getCoordinate(input[1]);
      log(c.x + " " + c.y);

      if (input[0] == "move") {
        log("here");
        return new Move("Step", null, c);
      } else if (input[0] == "wall") {
        string wallType = input[1][2] == 'h' ? "horizontal" : "vertical";
        return new Move("PutWall", wallType, c);
      }

      return null;

    }

    public void executeCommand(string[] input) {
      string command = input[0];

      if (command == "black" || command == "white") {
        Player player1 = new BotPlayer(0);
        Player player2 = new UserPlayer(1);

        if (command == "black") {
          Player[] playersArr = {
            player1,
            player2
          };

          List < Player > players = new List < Player > (playersArr);

          this.game = new Game(players);
        } else {
          Player[] playersArr = {
            player2,
            player1
          };

          List < Player > players = new List < Player > (playersArr);

          this.game = new Game(players);
        }
        game.start();
      } else if (command == "move" || command == "wall" || command == "jump") {
        Move move = getMove(input);
        string error = game.makeMove(move);
        log(error);
        if (!game.getIsOn() && game.winnerId != null) {
          Console.WriteLine(game.winnerId);
          log("Game over, winner: " + game.winnerId);
        }
      }
    }

    public void run() {
      Console.WriteLine("To start game enter start");

      string input = Console.ReadLine();

      while (input != "exit") {
        executeCommand(input.Split(" "));
        input = Console.ReadLine();
      }

      Console.WriteLine("Bye!");
    }

    public static void Main(String[] args) {
      GameCLI gameCli = new GameCLI();
      gameCli.run();
    }
  }
}