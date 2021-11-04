using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models
{
    public class ANode
    {
        public Move move { get; }
        public int rate;
        //            public PriorityQueue<int, ANode> children { get; set; }
        public List<ANode> children { get; set; }
        public ANode parent { get; set; }

        public ANode(Move move, int rate)
        {
            this.move = move;
            this.rate = rate;
            this.children = new List<ANode>();
        }
        public ANode(Move move, int rate, ANode parent)
        {
            this.move = move;
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