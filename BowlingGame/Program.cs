using System;
using BowlingGame.Service;

namespace BowlingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter player name:");
            var playerName = Console.ReadLine();
            
            var gameMode = GameMenu();

            while (gameMode != 3)
            {
                Console.WriteLine($"Press Enter {playerName} to Start");
                Console.ReadLine();
                Console.Clear();

                if (gameMode == 1)
                {
                    ManualGame(playerName);
                }
                else if (gameMode == 2)
                {
                    InteractiveGame(playerName);
                }

                gameMode = GameMenu();
            }
        }

        private static int GameMenu()
        {
            Console.Clear();
            Console.WriteLine("Availabe Game Modes");
            Console.WriteLine("1 - Manual Game");
            Console.WriteLine("2 - Interactive Game");
            Console.WriteLine("3 - To Exit Games");
            Console.WriteLine("");
            Console.Write("Desired Action? ");

            var x = Console.ReadLine();
            
            return int.Parse(x);
        }

        private static void InteractiveGame(string playerName)
        {
            var playService = new PlayGame();
            var frameIndx = 0;
            var rollCount = 0;

            playService.Start(Shared.Enums.PlayMode.Interactive);

            if (playService.IsStarted)
            {
                Console.WriteLine($"{playerName} your current score is {playService.GameScore}");
                Console.WriteLine("");
                Console.WriteLine($"Press Enter {playerName} to bowl");
                Console.ReadLine();

                while (!playService.IsFinished)
                {
                    var roll = playService.Bowl();

                    rollCount++;

                    Console.WriteLine($"{playerName} the roll {rollCount} bowled {roll.PinsBowled} pins.");

                    if (roll.RollType == Shared.Enums.RollType.Strike)
                    {
                        Console.WriteLine("You bowled a STRIKE.");
                    }
                    else if (roll.RollType == Shared.Enums.RollType.Spare)
                    {
                        Console.WriteLine("You bowled a Spare.");
                    }

                    Console.WriteLine("");
                    Console.WriteLine($"{playerName} your current frame score is {playService.FrameScore(frameIndx)}");
                    Console.WriteLine("");
                    Console.WriteLine($"Press Enter {playerName} for you next bowl");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine($"{playerName} your current score is {playService.GameScore}");
                    Console.WriteLine("");

                    if ((rollCount % 2) == 0)
                    {
                        frameIndx++;
                    }
                }

                Console.Clear();
                Console.WriteLine($"{playerName} your final score is {playService.GameScore}");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"Press Enter {playerName} to end the game.");

                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Game failed to start.");
                Console.WriteLine("Press Enter to Exit.");
                Console.ReadLine();
            }
        }

        private static void ManualGame(string playerName)
        {
            var playService = new PlayGame();
            var rollCount = 1;

            playService.Start(Shared.Enums.PlayMode.Manual);

            if (playService.IsStarted)
            {
                Console.Write($"Enter pins bowled on roll {rollCount}: ");
                var pinsBowled = Console.ReadLine();

                while (!playService.IsFinished)
                {
                    var roll = playService.TrackScore(int.Parse(pinsBowled));

                    if (roll.RollType == Shared.Enums.RollType.Strike)
                    {
                        Console.WriteLine("You bowled a STRIKE.");
                    }
                    else if (roll.RollType == Shared.Enums.RollType.Spare)
                    {
                        Console.WriteLine("You bowled a Spare.");
                    }

                    rollCount++;

                    Console.WriteLine("");
                    Console.WriteLine($"{playerName} your current score is {playService.GameScore}");
                    Console.WriteLine("");

                    if (!playService.IsFinished)
                    {
                        Console.Write($"Enter pins bowled on roll {rollCount}: ");
                        pinsBowled = Console.ReadLine();

                        Console.Clear();
                    }
                }

                Console.Clear();
                Console.WriteLine($"{playerName} your final score is {playService.GameScore}");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"Press Enter {playerName} to end the game.");

                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Game failed to start.");
                Console.WriteLine("Press Enter to Exit.");
                Console.ReadLine();
            }
        }
    }
}
