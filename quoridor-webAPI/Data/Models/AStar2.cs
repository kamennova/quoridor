using System;
using System.Collections.Generic;
using System.Text;

namespace quoridor_webAPI.Data.Models
{
    class AStar
    {
        private static int ManhattenDistance(Coordinate start, Coordinate end)
        {
            return Math.Abs(end.x - start.x) + Math.Abs(end.y - start.y);
        }

        private static int g(/*Coordinate point*/)
        {//way from current to point
            return 0;
        }
        private static int h(/*Coordinate point*/)
        {//way from point to end, presumably
            return 0;
        }
        private static int f(Coordinate c)
        {
            return h() + g();
        }

           private static void log(String s) {
              Console.WriteLine(s);
            }

        public static int search(GameState state, Coordinate start, int goalY) {
           PriorityQueue<ANode> closed = new PriorityQueue<ANode>();
           PriorityQueue<ANode> opened = new PriorityQueue<ANode>();

           opened.Enqueue(0, new ANode(start, 0));
           int length = -1;
           int counter = 0;

            while (opened.Count != 0) {
            counter++;
                int tempRate;
                ANode temp = opened.Dequeue(out tempRate);
//                log("Opened: " + temp.coordinate + " " + tempRate + " " + temp.rate);

                      if (temp.coordinate.y == goalY) {
                                        length = temp.rate;
                                        break;
                      }

                foreach (Move move in MoveValidator.getPossibleSimpleStepMoves(temp.coordinate, state)) {
                ANode searchNode = new ANode(move.coordinate, 0);
                        if(opened.Contains(searchNode) || closed.Contains(searchNode)) {
                                continue;
                        } else {
                        opened.Enqueue(Math.Abs(move.coordinate.y - goalY) * 5 + temp.rate + 1, new ANode(move.coordinate, temp.rate + 1));
                        }
                }
                closed.Enqueue(temp.rate, temp);
            }

            return length;
        }
    }
}