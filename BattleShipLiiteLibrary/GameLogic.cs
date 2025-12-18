using BattleShipLiiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipLiiteLibrary
{
    public static class GameLogic
    {

        public static void InitializeGrid(PlayerInfoModel model)
        {
            List<string> letters = new List<string>
            {
                "A","B","C","D","E"
            };


            List<int> numbers = new List<int>
            {  1, 2, 3, 4, 5 };


            foreach(string letter in letters)
            {
                foreach (int num in numbers)
                {
                    AddGridSpot(model, letter, num);
                }
            }

        }

        public static bool PlaceShip(PlayerInfoModel model, string location)
        {
            throw new NotImplementedException();
        } 

        private static void AddGridSpot(PlayerInfoModel mode, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel();

            spot.SpotLetter = letter;
            spot.SpotNumber = number;
            spot.Status = GridSpotStatusEnum.Empty;


            mode.ShotGrid.Add(spot);
        }

    }
}
