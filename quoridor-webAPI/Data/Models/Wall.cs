using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quoridor_webAPI.Data.Models
{
    public class Wall
    {
        public List<Coordinate> coordinate = new List<Coordinate>(capacity:2);
    }
}
