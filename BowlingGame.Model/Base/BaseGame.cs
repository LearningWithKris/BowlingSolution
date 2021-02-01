using BowlingGame.Shared.Enums;
using BowlingGame.Shared.Interface;
using System;
using System.Linq;

namespace BowlingGame.Model.Base
{
    public abstract class BaseGame : IGame
    {
        public string PlayerName { get; set; }

        //public virtual int Score { get => Frames.Where(f => (f != null) && (f.RollOne != null) && (f.RollTwo != null)).Sum(s => s.Total); }
        public virtual int Score { get => Frames.Where(f => f != null).Sum(s => s.Total); }

        public int RollCount { get; set; }
        
        public IFrame[] Frames { get; set; }

        public int CurrentFrame { get; set; }

        public DateTime PlayedOn { get; set; }

        public PlayMode PlayMode { get; set; }

        public virtual bool IsFinished { get; set; }

        public bool IsExtraRoll { get; private set; }

        public BaseGame() => BaseConfiguration();

        public BaseGame(string playerName)
        {
            BaseConfiguration();

            PlayerName = playerName;
        }

        public abstract IRoll Bowl();

        public virtual void Roll(IRoll roll)
        {
            try
            {
                #region Validation Checks

                if (IsFinished)
                {
                    throw new Exception("Invalid roll attempt.");
                }
                else if (roll.PinsBowled > 10)
                {
                    throw new Exception("Invalid number of pins recorded.");
                }

                #endregion

                // Track the number of rolls per game.
                RollCount++;

                // NOTE: If the RollCount divided by two has a remainder then a new frame needs to be created.
                //       A total of 21 rolls will be the maximum allowed.
                var isRollTypeUnknow = (roll.RollType == RollType.Unknown);

                if (RollCount == 21)
                {
                    Frames[9].ExtraPins = roll.PinsBowled;

                    if (isRollTypeUnknow)
                    {
                        roll.RollType = RollType.Extra;
                    }

                    IsFinished = true;
                }
                else if ((RollCount % 2) == 1)
                {
                    // First roll in a frame, 10 pins bowled equals strike, otherwise normal bowl.
                    if (isRollTypeUnknow)
                    {
                        roll.RollType = (roll.PinsBowled == 10) ? RollType.Strike : RollType.Normal;
                    }

                    Frames[CurrentFrame - 1] = new Frame() {RollOne = roll};

                    if (IsExtraRoll)
                    {
                        Frames[CurrentFrame - 2].ExtraPins = roll.PinsBowled;
                    }
                }
                else
                {
                    // Validation Check
                    if ((Frames[CurrentFrame - 1].RollOne.PinsBowled < 10) &&
                        (Frames[CurrentFrame - 1].RollOne.PinsBowled + roll.PinsBowled > 10))
                    {
                        throw new Exception("Invalid number of pins recorded on second roll.");
                    }

                    // NOTE: Special case if the first roll was a strike then a second bowl knocking 10 pins is a strike as well.
                    //       Second roll in a frame, 10 pins bowled or rollOne plus rollTwo total of ten pins bowled equals spare, otherwise normal bowl.
                    if (isRollTypeUnknow)
                    {
                        if (((Frames[CurrentFrame - 1].RollOne.PinsBowled) == 10) && (roll.PinsBowled == 10))
                        {
                            roll.RollType = RollType.Strike;
                        }
                        else
                        {
                            roll.RollType =
                                ((Frames[CurrentFrame - 1].RollOne.PinsBowled + roll.PinsBowled == 10) ||
                                 (roll.PinsBowled == 10))
                                    ? RollType.Spare
                                    : RollType.Normal;
                        }
                    }

                    Frames[CurrentFrame - 1].RollTwo = roll;

                    // Two bowled strike in a row still only gives one extra roll for scoring.
                    IsExtraRoll = (Frames[CurrentFrame - 1].RollOne.RollType == RollType.Strike) ||
                                  (roll.RollType == RollType.Spare);

                    CurrentFrame++;

                    IsFinished =
                        ((RollCount == 21) || ((RollCount == 20) && (Frames[9].RollTwo.RollType == RollType.Normal))) &&
                        !IsExtraRoll;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void BaseConfiguration()
        {
            PlayMode = PlayMode.Unknown;
            Frames = new IFrame[10];
            RollCount = 0;
            CurrentFrame = 1;
            IsExtraRoll = false;
        }
    }
}
