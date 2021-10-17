using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class Player
    {
        public int Id { get; set; }

        public Coordinates coordinates = new Coordinates(5, 0);

        public int amountOfWalls = 10;

    }
}
