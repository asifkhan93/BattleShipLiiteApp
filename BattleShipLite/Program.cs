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
                RecordPlayerShot(activePlayer, opponent);


                bool doesGameContinue = GameLogic.PlayerStillActive(opponent);
                if (doesGameContinue == true)
                {
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                else
                {
                    winner = activePlayer;
                    Console.WriteLine($"{winner.UserName} is the winner!");
                }

            } while (winner == null);


            IdentifyWinner(winner);

            Console.ReadLine();
        }

        private static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratualions to {winner.UserName} ");
            Console.WriteLine($"{winner.UserName} took {GameLogic.GetShotCount(winner)} shots");
        }

        private static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {

            bool isValidShot = false;
            string row = "";
            int column = 0;
            do
            {
                string shot = AskForShots();

                ( row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);

                isValidShot = GameLogic.ValdidateShot(activePlayer, row, column);

                if(isValidShot == false)
                {
                    Console.WriteLine("Invalid Shot!!! Please try again.");
                }


            } while (isValidShot == false);

            //Determine shot results
            bool isAHit = GameLogic.IdentifyShotShotResult(opponent, row, column);

            //Record results
            GameLogic.MarkShotResult(activePlayer, row, column, isAHit);

        }

        private static string AskForShots()
        {
            Console.WriteLine("Please Enter your Shot Selection");
            string output = Console.ReadLine();

            return output; 
        }

        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach (var gridspot in activePlayer.ShotGrid)
            {
                if (gridspot.SpotLetter != currentRow) 
                {
                    Console.WriteLine();
                    currentRow = gridspot.SpotLetter;
                }

                if (gridspot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridspot.SpotLetter}{gridspot.SpotNumber} ");
                }
                else if (gridspot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X ");
                }
                else if (gridspot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O ");
                }
                else
                {
                    Console.Write(" ? ");
                }

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
