using BowlingGame.Model;
using BowlingGame.Shared.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BowlingTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void CreateManualGamePlay()
        {
            // Arrange
            var manualPlay = PlayMode.Manual;

            // Act
            var game = new ManualGame();

            // Assert
            Assert.AreEqual(manualPlay, game.PlayMode, "Game play type isn't set to manual mode.");
        }

        [TestMethod]
        public void CreateManualGameWithPlayerName_PlayerOne()
        {
            var playerName = "Player One";
            var manualPlay = PlayMode.Manual;

            var game = new ManualGame();

            Assert.AreEqual(playerName, game.PlayerName, $@"The player for this game isn't {playerName}");
            Assert.AreEqual(manualPlay, game.PlayMode, "Game play type isn't set to manual mode.");
        }

        [TestMethod]
        public void CreateManualGameWithPlayerName()
        {
            var playerName = "Kris";
            var manualPlay = PlayMode.Manual;

            var game = new ManualGame("Kris");

            Assert.AreEqual(playerName, game.PlayerName, $@"The player for this game isn't {playerName}");
            Assert.AreEqual(manualPlay, game.PlayMode, "Game play type isn't set to manual mode.");
        }

        [TestMethod]
        public void ManualGameMutilpeRolls()
        {
            var frameOneTotal = 26;
            var frameTwoTotal = 19;
            var score = 54;

            var game = new ManualGame();

            game.Roll(new Roll() { PinsBowled = 10, RollType = RollType.Strike });
            game.Roll(new Roll() { PinsBowled = 8, RollType = RollType.Normal });
            game.Roll(new Roll() { PinsBowled = 8, RollType = RollType.Normal });
            game.Roll(new Roll() { PinsBowled = 2, RollType = RollType.Spare });
            game.Roll(new Roll() { PinsBowled = 9, RollType = RollType.Normal });

            Assert.AreEqual(score, game.Score, $@"Expected score {score} Game score {game.Score}");
            Assert.AreEqual(frameOneTotal, game.Frames[0].Total, $@"Expected total {frameOneTotal} Frame total {game.Frames[0].Total}");
            Assert.AreEqual(frameTwoTotal, game.Frames[1].Total, $@"Expected total {frameTwoTotal} Frame total {game.Frames[1].Total}");
        }

        [TestMethod]
        public void PerfectManualGame()
        {
            var score = 300;

            var game = new ManualGame();

            for (var i = 0; i < 21; i++)
            {
                game.Roll(new Roll() { RollType = RollType.Strike, PinsBowled = 10 });
            }

            Assert.AreEqual(score, game.Score, $@"Expected score {score} Game score {game.Score}");
        }

        [TestMethod]
        public void EnterPinsKnockedDown()
        {
            var bowl1 = 10;
            var bowl2 = 8;
            var bowl3 = 8;
            var bowl4 = 2;
            var bowl5 = 9;
            var bowl6 = 0;
            var score = 54;
            var frameOneTotal = 26;
            var frameTwoTotal = 19;
            var frameThreeTotal = 9;

            var game = new ManualGame();

            game.PinsBowled = bowl1;
            game.Bowl();

            game.PinsBowled = bowl2;
            game.Bowl();

            game.PinsBowled = bowl3;
            game.Bowl();

            game.PinsBowled = bowl4;
            game.Bowl();

            game.PinsBowled = bowl5;
            game.Bowl();

            game.PinsBowled = bowl6;
            game.Bowl();

            Assert.AreEqual(score, game.Score, $@"Expected score {score} Game score {game.Score}");
            Assert.AreEqual(frameOneTotal, game.Frames[0].Total, $@"Expected total {frameOneTotal} Frame total {game.Frames[0].Total}");
            Assert.AreEqual(frameTwoTotal, game.Frames[1].Total, $@"Expected total {frameTwoTotal} Frame total {game.Frames[1].Total}");
            Assert.AreEqual(frameThreeTotal, game.Frames[2].Total, $@"Expected total {frameThreeTotal} Frame total {game.Frames[2].Total}");
        }

        [TestMethod]
        public void OneRandomFrame()
        {
            // Track the random generated numbers to validate the frame total.
            var game = new RandomGame();

            var rollOnePinsBowled = game.Bowl().PinsBowled;
            var rollTwoPinsBowled = game.Bowl().PinsBowled;

            var score = rollOnePinsBowled + rollTwoPinsBowled;
            var frameTotal = rollOnePinsBowled + rollTwoPinsBowled;

            Assert.AreEqual(score, game.Score, $@"Expected score {score} Game score {game.Score}");
            Assert.AreEqual(frameTotal, game.Frames[0].Total, $@"Expected total {frameTotal} Frame total {game.Frames[0].Total}");
        }

        [TestMethod]
        public void RandomBowl()
        {
            var game = new RandomGame();

            var roll = game.Bowl();

            Assert.AreEqual(roll.PinsBowled, game.Frames[0].RollOne.PinsBowled, $@"Expected Pins Bowled {roll.PinsBowled}, Actual Pins Bowled {game.Frames[0].RollOne.PinsBowled}");
        }

        [TestMethod]
        public void ManualBowlNoPinsSent()
        {
            var pinsBowled = 0;

            var game = new ManualGame();

            var roll = game.Bowl();

            Assert.AreEqual(pinsBowled, roll.PinsBowled, $@"Expected Pins Bowled {pinsBowled}, Actual Pins Bowled {roll.PinsBowled}");
        }

        [TestMethod]
        public void RandomBowl_FrameSet()
        {
            var game = new RandomGame();

            var roll = game.Bowl();
            var frameSetPinsBowled = game.FrameSet.Where(p => p == 1).Sum();

            Assert.AreEqual(roll.PinsBowled, game.Frames[0].RollOne.PinsBowled, $@"Expected Pins Bowled {roll.PinsBowled}, Actual Pins Bowled {game.Frames[0].RollOne.PinsBowled}");
        }

        [TestMethod]
        public void RandomBowl_GameIsFinished()
        {
            var isFinished = true;
            
            var game = new RandomGame();
            var roll = game.Bowl();

            for (int i = 0; i < 19; i++)
            {
                roll = game.Bowl();
            }

            // Is an extra roll warranted?
            if (game.IsExtraRoll)
            {
                roll = game.Bowl();
            }

            Assert.AreEqual(isFinished, game.IsFinished, $@"Expected Game Finished {isFinished}, Actual Game Finished {game.IsFinished}");
            Assert.AreEqual(isFinished, game.IsFinished, $@"Expected Game Finished {isFinished}, Actual Game Finished {game.IsFinished}");
        }


        [TestMethod]
        public void ManualGameNoExtraRoll_GameIsFinished()
        {
            var isFinished = true;

            var game = new ManualGame();

            for (int i = 0; i < 20; i++)
            {
                game.PinsBowled = 4;
                game.Bowl();
            }

            Assert.AreEqual(isFinished, game.IsFinished, $@"Expected Game Finished {isFinished}, Actual Game Finished {game.IsFinished}");
        }
    }
}
