using BowlingGame.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame.Shared.Interface
{
    public interface IGame
    {
        PlayMode PlayMode { get; set; }

        string PlayerName { get; set; }

        int Score { get; }
        
        int RollCount { get; set; }

        bool IsFinished { get; set; }

        IFrame[] Frames { get; set; }

        int CurrentFrame { get; set; }

        DateTime PlayedOn { get; set; }

        void Roll(IRoll roll);

        IRoll Bowl();
    }
}
