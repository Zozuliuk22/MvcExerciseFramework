using Moq;
using NUnit.Framework;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using DAL;
using DAL.Interfaces;
using DAL.Enums;
using BLL.Guilds;
using BLL;
using BLL.NPCs;

namespace UnitTests
{
    [TestFixture]
    public class ThievesGuildTests
    {
        private PlayerLogic _player;
        private ThievesGuild _thievesGuild;

        [SetUp]
        public void SetUp()
        {
            _player = new PlayerLogic("Name");
            _thievesGuild = new ThievesGuild();
        }

        [Test]
        public void AddTheft_NewTheft_CurrentNumberTheftsOneMore()
        {
            var currentNumberThefts = _thievesGuild.CurrentNumberThefts;
            _thievesGuild.AddTheft();
            Assert.AreEqual(currentNumberThefts + 1, _thievesGuild.CurrentNumberThefts);
        }

        [Test]
        public void LoseGame_PlayerIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _thievesGuild.LoseGame(null));
        }

        [Test]
        public void PlayGame_PlayerIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _thievesGuild.PlayGame(null));
        }

        [Test]
        [TestCase(100)]
        public void PlayGame_PlayerDoesNotHaveEnoughMoney_PlayerIsAliveFalse(decimal startBudget)
        {
            _player.LoseMoney(startBudget);
            _thievesGuild.PlayGame(_player);
            Assert.IsFalse(_player.IsAlive);
        }

        [Test]
        [TestCase(100)]
        public void PlayGame_PlayerWinGame_PlayerLoseMoney(decimal startBudget)
        {
            _thievesGuild.PlayGame(_player);
            Assert.AreEqual(_player.CurrentBudget, startBudget - _thievesGuild.DefaultFee);
        }
    }
}



