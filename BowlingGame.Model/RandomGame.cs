using BowlingGame.Model.Base;
using BowlingGame.Shared.Enums;
using BowlingGame.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame.Model
{
    public class RandomGame : BaseGame
    {
        private readonly Random random = new Random();
        private int[] frameSet;
        private int availablePins = 10;

        public int[] FrameSet { get => frameSet; }

        public RandomGame() : base("Random Player One")
        {
            PlayMode = PlayMode.Interactive;
        }

        public RandomGame(string playerName) : base(playerName)
        {
            PlayMode = PlayMode.Interactive;
        }

        public override IRoll Bowl()
        {
            // Initialize the frame set.
            frameSet = new int[10];

            // Randomly determine the number of bowled pins.
            var roll = new Roll { PinsBowled = random.Next(0, availablePins) };

            Roll(roll);

            // After the frames first roll determine how pins sould be available for the frames second roll.
            if ((RollCount % 2) == 1)
            {
                // A strike on the first roll will make 10 pins available for the second roll.
                if (roll.RollType == RollType.Normal)
                {
                    availablePins -= roll.PinsBowled;
                }
            }
            else
            {
                // Frames second roll.
                availablePins = 10;
            }

            PinsBowled(roll.PinsBowled);

            return roll;
        }

        /// <summary>
        /// Create a frame set that shows which pins were knocked down on the roll.
        /// This is randomly generated to simulate an actual roll.
        /// Array position relates to pins number a zero value is standing pin a one value indicates a knocked down pin.
        /// </summary>
        /// <returns>Array representing the pins arrangement.</returns>
        private void PinsBowled(int pinsBowled)
        {
            // Randomly pick the specific pins knocked down for the number of pins bowled.
            for (int i = 0; i < pinsBowled; i++)
            {
                var specificPin = random.Next(0, 10);

                frameSet[specificPin] = 1;
            }
        }
    }
}
