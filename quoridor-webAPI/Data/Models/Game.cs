﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class Game
    {

    private Player[] players;

    private List<Coordinate> horizontalWallCoordinates;
    private List<Coordinate> verticalWallCoordinates;

       private string validateMove(Move move) {
       if (move.type == "PutWall" ) {
            return validateWallMove(move.coordinate, move.wallType);
       } else {
            return validateStepMove(move.coordinate);
       }
       }

        private string validateWallMove(Coordinate coordinate, string wallType) {
            // walls cannot intersect
            return null;
        }

        private string validateStepMove(Coordinate coordinate) {
            
            //player check
            if(coordinate.x == players[1].coordinate.x && coordinate.y == players[1].coordinate.y)
            {
                return "incorrect move";
            }
            //by y
            if (coordinate.x == players[0].coordinate.x)
            {
                if(Math.Abs(coordinate.y - players[0].coordinate.y) == 1)
                {
                    //wall check
                    //above player
                    if(coordinate.y - players[0].coordinate.y == 1){
                        for(int i = 0; i > horizontalWallCoordinates.Count; i++)
                        {
                            if (players[0].coordinate.x == horizontalWallCoordinates[i].x && (players[0].coordinate.y == horizontalWallCoordinates[i].coordinate.y))
                            {
                                return "incorrect move";
                            }
                        }
                    }
                    //below player
                    if(coordinate.y - players[0].coordinate.y == - 1){
                        for(int i = 0; i > horizontalWallCoordinates.Count; i++)
                        {
                            if (players[0].coordinate.x == horizontalWallCoordinates[i].x && ((players[0].coordinate.y - 1) == horizontalWallCoordinates[i].coordinate.y))
                            {
                                return "incorrect move";
                            }
                        }
                    }
                    return null
                }
                else{
                    // jump over
                    if(Math.Abs(coordinate.y - players[0].coordinate.y) == 2) 
                    {
                        if(players[0].coordinate.x == players[1].coordinate.x) // same line
                        {
                            if (Math.Abs(coordinate.y - players[1].coordinate.y) == 1) // jump over end near the second player
                            {
                                return null;
                            }
                            else{return "incorrect move";}
                        }
                    }
                    else{return "incorrect move";}
                }
            }
            //by x
            if (coordinate.y == players[0].coordinate.y)
            {
                if(Math.Abs(coordinate.x - players[0].coordinate.x) == 1)
                {
                    //wall check
                    //from right of a player
                    if(coordinate.x - players[0].coordinate.x == 1){
                        for(int i = 0; i > verticalWallCoordinates.Count; i++)
                        {
                            if (players[0].coordinate.y == verticalWallCoordinates[i].y && (players[0].coordinate.x == verticalWallCoordinates[i].coordinate.x))
                            {
                                return "incorrect move";
                            }
                        }
                    }
                    //from left of a player
                    if(coordinate.x - players[0].coordinate.x == - 1){
                        for(int i = 0; i > verticalWallCoordinates.Count; i++)
                        {
                            if (players[0].coordinate.y == verticalWallCoordinates[i].y && ((players[0].coordinate.x - 1) == verticalWallCoordinates[i].coordinate.x))
                            {
                                return "incorrect move";
                            }
                        }
                    }
                    return null
                }
                else{
                    // jump over
                    if(Math.Abs(coordinate.x - players[0].coordinate.x) == 2)
                    {
                        if(players[0].coordinate.y == players[1].coordinate.y) // same line
                        {
                            if (Math.Abs(coordinate.x - players[1].coordinate.x) == 1) // jump over end near the second player
                            {
                                return null;
                            }
                            else{return "incorrect move";}
                        }
                    }
                    else{return "incorrect move";}
                }
            }
            return "incorrect move";
        }

       public string makeMove(Move move) {
        string moveError = validateMove(move);
         return moveError;
       }


       public bool isOn = false;

       public bool getIsOn() {
            return isOn;
       }

       private int winnerId { get; set; }

       public int getWinnerId(){
        return winnerId;
       }
    }
}
