using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using quoridor_webAPI.Data.Models;
using quoridor_webAPI.Data.ViewModels;

namespace quoridor_webAPI.Data.Services
{
    public class PlayerService
    {
        private Player _player;
        public void AddPlayer(PlayerVM player)
        {
            _player = new Player()
            {
                Id = 1,
                coordinates = player.Coordinates,
                amountOfWalls = player.amountOfWalls
            };

        }

        public void UpdatePlayer(PlayerVM player)
        {
            _player.coordinates = player.Coordinates;
            _player.amountOfWalls = player.amountOfWalls;
        }

        public Coordinates GetValidPosition()
        {
            //A* algorithm

            return new Coordinates(0, 0);
        }
    }
}
