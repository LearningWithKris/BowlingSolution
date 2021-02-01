using BowlingGame.Model;
using BowlingGame.Shared.Enums;
using BowlingGame.Shared.Interface;
using System;

namespace BowlingGame.Service
{
    public class PlayGame
    {
        private IGame game;
        private bool isStarted = false;

        public bool IsStarted { get => isStarted; }

        public int FrameScore(int frame) => game.Frames[frame].Total;

        public int GameScore { get => game.Score; }

        public bool IsFinished { get => game.IsFinished; }

        public void Start(PlayMode playMode)
        {
            try
            {
                switch (playMode)
                {
                    case PlayMode.Unknown:
                        isStarted = false;
                        break;

                    case PlayMode.Manual:
                        game = new ManualGame();

                        isStarted = true;
                        break;

                    case PlayMode.Interactive:
                        game = new RandomGame();

                        isStarted = true;
                        break;

                    default:
                        isStarted = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                isStarted = false;

                throw ex;
            }
        }

        public IRoll TrackScore(int pinsBowled)
        {
            (game as ManualGame).PinsBowled = pinsBowled;

            var roll = game.Bowl();

            return roll;
        }

        public IRoll Bowl()
        {
            return game.Bowl();
        }
    }
}
