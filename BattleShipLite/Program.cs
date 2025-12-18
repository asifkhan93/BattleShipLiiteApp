using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShipLiiteLibrary;
using BattleShipLiiteLibrary.Models;

namespace BattleShipLite
{
    class Program
    {
        static void Main(string[] args)
        {

            WelcomeMessage();

            PlayerInfoModel activePlayer = CreatePlayer("Player 1");
            PlayerInfoModel opponent = CreatePlayer("Player 2");
            PlayerInfoModel winner = null;

            do
            {
                // Display grid from activePlayer on where they fired
                DisplayShotGrid(activePlayer);
                // Ask activePlayer for a shot
                // Determine if it is a valid shot
                // Determine shot results
                //Determine if the game is over
                //if over, sert winner to activePlayer
                //else , swap positions (activePlayer to opponent)

            } while (winner == null);

            Console.ReadLine();
        }

        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            foreach (var gridspot in activePlayer.ShotGrid)
            {

            }
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to BattleShip Lite!");
            Console.WriteLine();
        }

        private static PlayerInfoModel CreatePlayer(string playerTitle) 
        {
            PlayerInfoModel output = new PlayerInfoModel();

            Console.WriteLine($"Player information for { playerTitle}");

            // Ask for user name
            output.UserName = AskForUsersName();
            //Load up the shot grid
            GameLogic.InitializeGrid(output);
            //Ask the user to place ships
            PlaceShips(output);
            //clear
            Console.Clear(); 
            
            return output;
        }

        private static string AskForUsersName()
        {
            Console.Write("Please enter your name: ");
            string output = Console.ReadLine();

            return output;
        }

        private static void PlaceShips( PlayerInfoModel model)
        {
            do
            {
                Console.Write($"Where do you want to place ship number {model.ShipLocations.Count + 1}: ");
                string location = Console.ReadLine();

                bool isValidLocation = GameLogic.PlaceShip(model, location);
                if (isValidLocation == false)
                {
                    Console.WriteLine("This was not a valid location. Please Try again");
                }
            } while (model.ShipLocations.Count < 5);
        }
    }
}
