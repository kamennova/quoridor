using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    class Astar
    {
        List<Coordinate> closed;
        PriorityQueue<int, Coordinate> opened;

        double Distance(Coordinate start, Coordinate end)
        {
            return Math.Sqrt((end.x - start.x)^2 +(end.y - start.y)^2);
        }

        void search(Coordinate start, Coordinate goal)
        {

        }
        
    }
}

