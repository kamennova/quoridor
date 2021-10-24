using System;
using System.Collections.Generic;
using System.Linq;

namespace quoridor_webAPI.Data.Models
{
    public class BaseMinimax
    {

        bool isAllyTurn { get; set; }

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