using BowlingGame.Model.Base;
using BowlingGame.Shared.Enums;
using BowlingGame.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame.Model
{
    public class ManualGame : BaseGame
    {
        public ManualGame() : base("Player One")
        {
            PlayMode = PlayMode.Manual;
        }

        public ManualGame(string playerName) : base(playerName)
        {
            PlayMode = PlayMode.Manual;
        }

        public int PinsBowled { get; set; }

        public override IRoll Bowl()
        {
            var roll = new Roll() { PinsBowled = PinsBowled };

            Roll(roll);

            return roll;
        }
    }
}
