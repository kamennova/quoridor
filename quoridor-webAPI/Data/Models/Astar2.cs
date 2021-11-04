using System;
using System.Collections.Generic;
using System.Text;

namespace quoridor_webAPI.Data.Models
{
    class Astar2
    {
        PriorityQueue<int, Coordinate> closed;
        PriorityQueue<int, Coordinate> opened;
        ANode Way;
        Coordinate current;
        int finish;

        public Astar2(Coordinate startPosition, int goalY)
        {
            closed = new PriorityQueue<int, Coordinate>();
            opened = new PriorityQueue<int, Coordinate>();
            current = startPosition;
            this.finish = goalY;
            opened.Enqueue(0, startPosition);
        }

        int ManhattenDistance(Coordinate start, Coordinate end)
        {
            return Math.Abs(end.x - start.x) + Math.Abs(end.y - start.y);
        }

        List<Move> GetPossibleMoves(Coordinate positopn)
        {
            return new List<Move>();
        }

        int g(/*Coordinate point*/)
        {//way from current to point
            return 0;
        }
        int h(/*Coordinate point*/)
        {//way from point to end, presumably
            return 0;
        }
        int f(Coordinate Move)
        {
            return h() + g();
        }

        PriorityQueue<int, Coordinate> GetWay()
        {
            while (opened.Count != 0)
            {
                Coordinate temp = opened.Dequeue();
                ANode q = new ANode(temp, f(temp.coordinate));
                foreach (Move move in GetPossibleMoves(q.move.coordinate))
                {
                    q.Insert(new ANode(move, f(move.coordinate)));
                    if (finish == move.coordinate.y)
                    {
                        return closed;
                    }
                    if (opened.Contains(move))
                    {
                        int oldPriority;
                        if (opened.TryGetPriority(move, out oldPriority))
                        {
                            if (oldPriority < f(move.coordinate))
                            {
                                continue;
                            }
                        }
                    }
                    if (closed.Contains(move.coordinate))
                    {
                        int oldPriority;
                        if (closed.TryGetPriority(move.coordinate, out oldPriority))
                        {
                            if (oldPriority < f(move.coordinate))
                            {
                                continue;
                            }
                        }
                    }
                    opened.Enqueue(0, move);
                }
                closed.Enqueue(q.rate, q.move.coordinate);
            }
            return closed;
        }
    }
}
