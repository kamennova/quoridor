using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models
{
    public class ANode
    {
        public Coordinate coordinate { get; }
        public int rate;
        //            public PriorityQueue<int, ANode> children { get; set; }
        public List<ANode> children { get; set; }
        public ANode parent { get; set; }

        public ANode(Coordinate c, int rate)
        {
            this.coordinate = c;
            this.rate = rate;
            this.children = new List<ANode>();
        }
        public ANode(Coordinate c, int rate, ANode parent)
        {
            this.coordinate = c;
            this.rate = rate;
            this.children = new List<ANode>();
            this.parent = parent;
        }
        public void Insert(ANode node)
        {
            children.Add(node);
            //                children.Enqueue(node.rate, node);
        }
    }
}