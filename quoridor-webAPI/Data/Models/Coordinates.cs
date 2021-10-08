using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class Coordinates
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
