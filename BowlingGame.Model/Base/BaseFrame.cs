using BowlingGame.Shared.Enums;
using BowlingGame.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame.Model.Base
{
    public abstract class BaseFrame : IFrame
    {
        public IRoll RollOne { get; set; }

        public IRoll RollTwo { get; set; }

        public int ExtraPins { get; set; }

        public virtual int Total { get => ((RollOne == null) ? 0 : RollOne.PinsBowled) + ((RollTwo == null) ? 0 : RollTwo.PinsBowled) + ExtraPins; }
    }
}
