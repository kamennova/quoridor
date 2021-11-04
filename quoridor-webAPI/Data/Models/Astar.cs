using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    class AStar
    {
        GameState gameState;
        //todo whith dicision tree for reconstruct path function
        Coordinate start, end, current;
        List<Move> closed;
        ANode theWay;
        PriorityQueue<int, Move> opened;

        public AStar(Coordinate start, GameState gameState)
        {
            this.start = start;
            current = start;
            end  = new Coordinate (start.x, Math.Abs(start.y - 8));
            this.gameState = gameState;
            this.theWay = new ANode(new Move("starter", null, start), 0);
            opened.Enqueue(0, new Move("starter", null, start));
        }
        // heuristic? 
        int g(Coordinate point) // g(x)
        {//calculate value of way from the start to point
            return Distance(start,point);
        }
        int h(Coordinate point)
        {//calculate value of way from the start to point
            return Distance (current,point);
        }
        int f(Coordinate point) ////f(x) = g(x) + h(x)
        {//the total value of way
            return g(point) + h(point);
        }
        int Distance(Coordinate start, Coordinate end)
        {//return Distance between dots
            //manhatten distance
            return Math.Abs(end.x - start.x) + Math.Abs(end.y - start.y);
        }
        /*
        idk what the hell it was
        double Distance(Coordinate start, int goal)
        {
            return Math.Abs(start.y - goal);
        }*/

        // goal is y axis value, return distance to goal or -1

        
        List<Move> PossibleMoves(Coordinate c)
        {
            return MoveValidator.getPossibleSimpleStepMoves(c, gameState);
        }
        public ANode GetRoot()
        {
            ANode temp = theWay;
            while (temp.parent != null)
            {
                temp = temp.parent;
            }
            return temp;
        }
        private Move Close()
        {
            Move temp = opened.Dequeue();
            closed.Add(temp);
            return temp;
        }
        public ANode GetWay()
        {
            
            while (opened.Count != 0)
            {
                Move bestMove = opened.Dequeue();
                theWay.Insert(new ANode(bestMove, f(bestMove.coordinate)));
                theWay = theWay.children[0];
                current = bestMove.coordinate;
                if (current == end)
                {
                    return theWay;
                }
                opened.Clear();
                foreach (Move move in PossibleMoves(current))
                {
                    opened.Enqueue(f(move.coordinate), move);
                }
            }
            return null; // did not find way
        }

        void ReconstructPath()
        {
            //todo
        }

        //idk what the hell it is?
        public static int search(GameState state, Coordinate start, int goal) {
            return Math.Abs(start.y - goal);
        }
        

    }
}

