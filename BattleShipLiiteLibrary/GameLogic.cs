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

        public static bool PlayerStillActive(PlayerInfoModel player)
        {
            bool isActive = false;

            foreach(var ship in player.ShipLocations)
            {
                if (ship.Status != GridSpotStatus.Sunk  )
                {
                    isActive = true;
                }
            }

            return isActive;
        }
        public static int GetShotCount(PlayerInfoModel player)
        {
            int shotCount = 0;

            foreach (var spot in player.ShotGrid)
            {
                if (spot.Status != GridSpotStatus.Empty)
                {
                    shotCount += 1;
                }
            }

            return shotCount;
        }
        public static bool PlaceShip(PlayerInfoModel model, string location)
        {
            bool Ouput = false;
            (string row, int column) = SplitShotIntoRowAndColumn(location);


            bool isValidLocation = validateGridLocation(model, row, column);
            bool isSpotOpen = validateShipLocation(model, row, column);

            if (isValidLocation && isSpotOpen)
            {
                model.ShipLocations.Add(new GridSpotModel
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    Status = GridSpotStatus.Ship
                });

                Ouput = true;
            }


            return Ouput;
        }

        private static bool validateGridLocation(PlayerInfoModel model, string row, int column)
        {
            bool isValidLocation = false;

            foreach (var ship in model.ShotGrid)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidLocation = true;
                }
            }

            return isValidLocation;
        }

        private static bool validateShipLocation(PlayerInfoModel model, string row, int column)
        {
            bool isValidLocation = true;

            foreach (var ship in model.ShipLocations) 
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidLocation = false;
                }
            }

            return isValidLocation;
        }

        private static void AddGridSpot(PlayerInfoModel mode, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel();

            spot.SpotLetter = letter;
            spot.SpotNumber = number;
            spot.Status = GridSpotStatus.Empty;


            mode.ShotGrid.Add(spot);
        }



        public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
        {
            //string[] shotArray = shot.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string row = "";
            int column = 0;

            if (shot.Length != 2)
            {
                throw new ArgumentException("This was an invalid shot type.", "shot");
            }

            char[] shotArray = shot.ToArray();

            row = shotArray[0].ToString().ToUpper();
            column = int.Parse(shotArray[1].ToString());


            return (row, column);
        }

        public static bool ValdidateShot(PlayerInfoModel Player, string row, int column)
        {
            bool isValidShot = false;

            foreach (var gridSpot in Player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if (gridSpot.Status == GridSpotStatus.Empty)
                    {
                        isValidShot = true;
                    }
                }
            }

            return isValidShot;
        }

        public static bool IdentifyShotShotResult(PlayerInfoModel opponent, string row, int column)
        {
            bool isAHit = false;

            foreach (var ship in opponent.ShipLocations)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isAHit = true;
                }
            }

            return isAHit;
        }

        public static void MarkShotResult(PlayerInfoModel Player, string row, int column, bool isAHit)
        {
            

            foreach (var gridSpot in Player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if(isAHit)
                    {
                        gridSpot.Status = GridSpotStatus.Hit;
                    }
                    else
                    {
                        gridSpot.Status = GridSpotStatus.Miss;
                    }   
                }
            }

        }
    }
}
