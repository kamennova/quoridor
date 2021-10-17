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
}
}