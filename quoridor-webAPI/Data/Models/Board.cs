using System;
using System.Collections.Generic;

namespace quoridor_webAPI.Data.Models
{
public class Board {

    private List<Coordinate> horizontalWallCoordinates = new List<Coordinate>();
    private List<Coordinate> verticalWallCoordinates = new List<Coordinate>();

    public List<Coordinate> getVerticalWalls(){
        return verticalWallCoordinates;
    }

    public List<Coordinate> getHorizontalWalls(){
        return horizontalWallCoordinates;
    }

    public void putWall(string wallType, Coordinate coordinate) {
        if (wallType == "vertical") {
            verticalWallCoordinates.Add(coordinate);
        } else {
            horizontalWallCoordinates.Add(coordinate);
        }
    }

    public void applyWallMove(Move move) {
//     if (move.type == "PutWall") {
                    if (move.wallType == "horizontal") {
                        this.getHorizontalWalls().Add(move.coordinate);
                    } else {
                        this.getVerticalWalls().Add(move.coordinate);
                    }
//                } else {
//                    this.players[currentTurn].coordinate = move.coordinate;
//                }
    }
}
}