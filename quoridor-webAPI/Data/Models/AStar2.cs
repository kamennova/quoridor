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
        return Math.Abs(start.y - goalY);
        PriorityQueue<Coordinate> closed  = new PriorityQueue<Coordinate>();
                PriorityQueue<Coordinate> opened = new PriorityQueue<Coordinate>();

           opened.Enqueue(0, start);
           int length = -1;

            while (opened.Count != 0) {
                int tempRate;
                Coordinate temp = opened.Dequeue(out tempRate);

                ANode q = new ANode(temp, f(temp));
//                log("Opened: " + temp + " " + tempRate);

                      if (temp.y == goalY) {
                                        length = tempRate;
                                        break;
                      }

                foreach (Move move in MoveValidator.getPossibleSimpleStepMoves(temp, state)) {
//                    q.Insert(move.coordinate, f(move.coordinate));
                        if(opened.Contains(move.coordinate)) {
//                        int oldPriority = -1; // todo why?
//                        if (opened.TryGetPriority(move.coordinate, out oldPriority)) {
//                            if (oldPriority < f(move.coordinate)) {
                                continue;
//                            }
//                        }
                        } else if (closed.Contains(move.coordinate)) {
//                        oldPriority = -1;
//                        if (closed.TryGetPriority(move.coordinate, out oldPriority)) {
//                            if (oldPriority < f(move.coordinate)) {
//                                continue;
//                            }
//                        }
                        } else {
                        opened.Enqueue(tempRate + 1, move.coordinate);
                        }
                }

                closed.Enqueue(tempRate, temp);
            }

            return length;
        }
    }
}