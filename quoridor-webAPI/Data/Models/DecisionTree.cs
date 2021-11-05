using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models
{
    public class Node
        {
            public Move move { get; }
            public double rate;
//            public PriorityQueue<int, Node> children { get; set; }
            public List<Node> children { get; set; }

            public Node (Move move, double rate) {
                this.move = move;
                this.rate = rate;
                this.children = new List<Node> ();
            }

            public void Insert(Node node) {
                    children.Add(node);
//                children.Enqueue(node.rate, node);
            }
        }
}