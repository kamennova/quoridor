using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using quoridor_webAPI.Data.Models;

namespace quoridor_webAPI.Data.ViewModels
{
    public class GameStart
    {
        public List<Coordinate> coordinates;
        public GameStart(List<Coordinate> c) {
            this.coordinates = c;
        }
    }
}