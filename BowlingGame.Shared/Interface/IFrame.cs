using BowlingGame.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame.Shared.Interface
{
    public interface IFrame
    {
        IRoll RollOne { get; set; }

        IRoll RollTwo { get; set; }

        int ExtraPins { get; set; }

        int Total { get; }
    }
}
