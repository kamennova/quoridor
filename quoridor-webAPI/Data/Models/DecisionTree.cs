using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models
{
    public class Node
        {
            public bool isAllyTurn { get; } // true == your turn; false ==enemy turn
            Move currentPosition { get; }
            //Node parantNode { get; set; }
            List<Node> childNodes { get; set; }

            public Node (Move currentPosition, bool isAllyTurn)
            {
                this.currentPosition = currentPosition;
                this.isAllyTurn = isAllyTurn;
            }
            public void Insert(Node currentMove) {
                if (!childNodes.Contains(currentMove))
                {
                    childNodes.Add(currentMove);
                }
            }
            public Node OneLevelSearch(Move desiredMove) //not deep, but fast
            {
                foreach(Node node in childNodes)
                {
                    if (desiredMove.Equals(node.currentPosition))
                    {
                        return node;
                    }
                }
                return null;
            }

            public Node DeepSearch(Node currentNode, Move desiredMove) //deep, but not fast
            {
                if(currentNode.currentPosition == desiredMove)
                { return currentNode; }
                if (currentNode.childNodes.Count != 0)
                {
                    foreach (Node node in currentNode.childNodes)
                    {
                        Node temp = DeepSearch(node, desiredMove);
                        if (temp != null) { return temp; }
                    }
                    return null;
                }
                else return null;

            }

            public Move GetCurrent() { return currentPosition; }
        }   
}