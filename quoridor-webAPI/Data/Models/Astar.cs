using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    class AStar
    {
        List<Coordinate> closed;
        PriorityQueue<int, Coordinate> opened;

        // heuristic?
        double Distance(Coordinate start, int goal)
        {
            return Math.Abs(start.y - goal);
        }

        // goal is y axis value, return distance to goal or -1
        public static int search(Board board, Coordinate start, int goal) {
            return 0;
        }
    }
}

