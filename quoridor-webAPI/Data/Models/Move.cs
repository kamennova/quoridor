using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class Move
    {
       public bool isOn = false;
       public Coordinates coordinates = new Coordinates(5, 0);

       public Move(){

       }
    }
}
