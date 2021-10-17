using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class Move
    {
       public bool isOn = false;
       public Coordinate coordinate = new Coordinate(5, 0);
       public string type;
       public string wallType;

       public Move(string moveType, string wallType, Coordinate coordinate){
            this.type = moveType;
            this.wallType = wallType; // can be null, bad i know
       }
    }
}
