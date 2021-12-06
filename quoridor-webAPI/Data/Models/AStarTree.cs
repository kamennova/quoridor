using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models
{
    public class ANode
    {
        public Coordinate coordinate { get; }
        public int rate;

        public ANode(Coordinate c, int rate)
        {
            this.coordinate = c;
            this.rate = rate;
        }

          public override bool Equals(object obj)
                        {
                            if (obj == null) return false;
                            ANode c = obj as ANode;
                            return c.coordinate.Equals(this.coordinate); // bad
                        }

              public override int GetHashCode()
                                    {
            return this.coordinate.GetHashCode();
                                    }
    }
}