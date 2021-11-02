using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    class AStar
    {
        //todo whith dicision tree for reconstruct path function
        Coordinate start, end, current;
        List<Move> closed;
        PriorityQueue<double, Move> opened;

        public AStar(Coordinate start)
        {
            this.start = start;
            current = start;
            end  = new Coordinate (start.x, Math.Abs(start.y - 8));
        }
        // heuristic? 
        double g(Coordinate point) // g(x)
        {//should calculate value of way from the start to point
            return Distance(start,point);
        }
        double h(Coordinate point)
        {//heuristic 
            return Distance (current,point);//todo other heuristic?
        }
        double f(Coordinate point) ////f(x) = g(x) + h(x)
        {//the total value of way
            return g(point) + h(point);
        }
        double Distance(Coordinate start, Coordinate end)
        {//return Distance between dots
            return Math.Sqrt((end.x - start.x)^2 + (end.y - start.y)^2);
        }
        /*
        idk what the hell it was
        double Distance(Coordinate start, int goal)
        {
            return Math.Abs(start.y - goal);
        }*/

        // goal is y axis value, return distance to goal or -1

        void Open(Move Step)
        {//open node, complications possible!
            opened.Enqueue(f(Step.coordinate), Step);
        }
        Move Close()
        {
            Move temp =  opened.Dequeue();
            closed.Add(temp);
            return temp;
        }
        List<Move> PossibleMoves(Coordinate c)
        {
            List<Move> NullPoint = new List<Move>();
            return NullPoint; //todo
        }
        public List<Move> GetWay()
        {
            Move starter = new Move ("setPosition", null, start);
            Open (starter);
            while(opened.Count != 0)
            {
                current = Close().coordinate;
                
                if(current == end)
                {
                    return closed; //todo reconstruct path function?
                }
                foreach(Move move in PossibleMoves(current))
                {
                    Open(move);
                }
            }
            return null; // did not find way
        }

       
       //idk what the hell it is?
        public static int search(GameState state, Coordinate start, int goal) {
            return Math.Abs(start.y - goal);
        }
        

    }
}

