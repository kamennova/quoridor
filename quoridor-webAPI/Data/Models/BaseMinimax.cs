using System;
using System.Collections.Generic;
using System.Linq;

namespace quoridor_webAPI.Data.Models
{
    public class BaseMinimax
    {

        bool isAllyTurn { get; set; }

        private List<Move> getPossibleMoves(Player player, Board board, List<Player> players) {
            List<Move> steps = getPossibleStepMoves( player,  board, players);

            List<Move> walls = getPossibleWallMoves(player, board,  players);

            return steps;
        }

                private List<Move> getPossibleStepMoves(Player player, Board board, List<Player> players){

                    Coordinate c = player.coordinate;
                    List<Move> moves = new List<Move>();
                    if (c.y < 8 && MoveValidator.checkWallsToTheTop(c, board.getHorizontalWalls())) { // check top
                    Coordinate c2 = new Coordinate(c.x, c.y + 1);
                    Move move = new Move("Move", null, c2);
                        moves.Add(move);
                    }

                    if (c.y > 0 && !MoveValidator.checkWallsToTheBottom(c, board.getHorizontalWalls())){
                        moves.Add(new Move("Move", null, new Coordinate(c.x, c.y-1)));
                                    }

                    return moves;
                }

        private List<Move> getPossibleWallMoves(Player player, Board board, List<Player> players) {

        List<Move> moves = new List<Move>();
            if (player.amountOfWalls == 0) {

            }

            return moves;

        }

        int EvaluateTheMove(Move move) // return value of move
        {
            //ehristic algorithm?
            return 0;
        }
        

        
        public Move ChooseMove()
        {
            Coordinate currentCoordinate = new Coordinate(0, 0);
            Move currentMove = new Move("null", "null", currentCoordinate);
            Node root = new Node(currentMove, isAllyTurn); //root of tree
            //root.Insert(Node newMove)
            //fill child nodes with variants
            PriorityQueue<int, Node> possibleMovies = new PriorityQueue<int, Node>();//store possible moves before alpha or bete pruning
            foreach (Node node in root.childNodes)
            {
                possibleMovies.Enqueue(EvaluateTheMove(node.currentPosition), node); //set value for every move
            }
            //finish function
            return possibleMovies.Dequeue().currentPosition;
        }
    }
}