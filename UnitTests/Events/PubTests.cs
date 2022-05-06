using NUnit.Framework;
using System;
using BLL.Events;
using BLL;

namespace UnitTests
{
    [TestFixture]
    public class PubTests
    {
        private Pub _pub;
        private PlayerLogic _player;

        [SetUp]
        public void SetUp()
        {
            _pub = new Pub();
            _player = new PlayerLogic("Name");
        }

        [Test]
        public void PlayGame_PlayerHasEnoughMoney_PlayerCurrentBeersPlusOne()
        {
            var startAmountOfBeers = _player.CurrentBeers;
            _pub.PlayGame(_player);
            Assert.AreEqual(startAmountOfBeers + 1, _player.CurrentBeers);
        }

        [Test]
        public void PlayGame_PlayerIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _pub.PlayGame(null));
        }
    }
}
