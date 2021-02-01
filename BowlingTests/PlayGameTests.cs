using BowlingGame.Service;
using BowlingGame.Shared.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingTests
{
    [TestClass]
    public class PlayGameTests
    {
        [TestMethod]
        public void StartManualGame()
        {
            var gameStarted = true;

            var game = new PlayGame();

            game.Start(PlayMode.Manual);

            Assert.AreEqual(gameStarted, game.IsStarted, $@"Expected result: {gameStarted}, Actual result: {game.IsStarted}");
        }

        [TestMethod]
        public void ManualGameNotStarted()
        {
            var gameStarted = false;

            var game = new PlayGame();

            Assert.AreEqual(gameStarted, game.IsStarted, $@"Expected result: {gameStarted}, Actual result: {game.IsStarted}");
        }

        [TestMethod]
        public void ManualGame_TrackScoreStrike()
        {
            var pinsBowled = 10;
            var rollType = RollType.Strike;

            var game = new PlayGame();

            game.Start(PlayMode.Manual);
            var roll = game.TrackScore(pinsBowled);

            Assert.AreEqual(pinsBowled, roll.PinsBowled, $@"Expected result: {pinsBowled}, Actual result: {roll.PinsBowled}");
            Assert.AreEqual(pinsBowled, game.FrameScore(0), $@"Expected result: {pinsBowled}, Actual result: {game.FrameScore(0)}");
            Assert.AreEqual(rollType, roll.RollType, $@"Expected result: {rollType}, Actual result: {roll.RollType}");
        }

        [TestMethod]
        public void ManualGame_BackToBackStrikes()
        {
            var pinsBowled = 10;
            var rollType = RollType.Strike;

            var game = new PlayGame();

            game.Start(PlayMode.Manual);
            var rollOne = game.TrackScore(pinsBowled);
            var rollTwo = game.TrackScore(pinsBowled);

            Assert.AreEqual(pinsBowled, rollOne.PinsBowled, $@"Expected result: {pinsBowled}, Actual result: {rollOne.PinsBowled}");
            Assert.AreEqual((pinsBowled * 2), game.FrameScore(0), $@"Expected result: {(pinsBowled * 2)}, Actual result: {game.FrameScore(0)}");
            Assert.AreEqual(rollType, rollOne.RollType, $@"Roll One Expected result: {rollType}, Actual result: {rollOne.RollType}");
            Assert.AreEqual(rollType, rollTwo.RollType, $@"Roll Two Expected result: {rollType}, Actual result: {rollTwo.RollType}");
        }
    }
}
