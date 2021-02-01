using BowlingGame.Shared.Enums;
using BowlingGame.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame.Model
{
    public class Roll : IRoll
    {
        public RollType RollType { get; set; }

        public int PinsBowled { get; set; }

        public Roll() => RollType = RollType.Unknown;
    }
}
