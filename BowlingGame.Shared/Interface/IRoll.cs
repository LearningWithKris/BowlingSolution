using BowlingGame.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame.Shared.Interface
{
    public interface IRoll
    {
        RollType RollType { get; set; }

        int PinsBowled { get; set; }
    }
}
