using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class Coordinate
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

         public override bool Equals(object obj)
                {
                    if (obj == null) return false;
                    Coordinate c = obj as Coordinate;
                    return c.x == this.x && c.y == this.y;
                }

         public override string ToString() {
            return "( " + x + ", " + y + " )";
         }

         public override int GetHashCode() {
            return x * 10 + y;
         }
    }
}
