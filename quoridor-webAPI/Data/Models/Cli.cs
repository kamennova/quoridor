using System;
using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models {

  public class GameCLI {
    private Game game;
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

    private void log(String s) {
      Console.WriteLine(s);
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

      if (command == "black" || command == "white") {
        if (command == "black") {
          Player[] playersArr = {
            player,
            bot
          };

          List < Player > players = new List < Player > (playersArr);

          this.game = new Game(players);
        } else {
          Player[] playersArr = {
          bot,
            player
          };

          List < Player > players = new List < Player > (playersArr);

          this.game = new Game(players);
        }
        game.start();
      } else if (command == "move" || command == "wall" || command == "jump") {
        Move move = getMove(input);
        string error = game.makeMove(move);
        log(error);
      }
    }

    public void run() {
      Console.WriteLine("To start game enter black or white");

      string input;

      do {
        input = Console.ReadLine();
        executeCommand(input.Split(" "));

        if (!game.getIsOn() && game.winnerId != null) {
          Console.WriteLine(game.winnerId);
          log("Game over, winner: " + game.winnerId);
        }

        if (game.players[game.getTurn()].Id == 0) { // todo

          game.makeMove(bot.getMove(game.board, game.players));

          if (!game.getIsOn() && game.winnerId != null) {
            Console.WriteLine(game.winnerId);
            log("Game over, winner: " + game.winnerId);
          }
        }

      } while (input != "exit");

      Console.WriteLine("Bye!");
    }

    public static void Main(String[] args) {
      GameCLI gameCli = new GameCLI();
      gameCli.run();
    }
  }
}
