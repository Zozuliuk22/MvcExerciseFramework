using NUnit.Framework;
using System;
using BLL.NPCs;

namespace UnitTests
{
    [TestFixture]
    public class AssassinNpcTests
    {
        private AssassinNpc _npc;

        [SetUp]
        public void SetUp()
        {
            _npc = new AssassinNpc();
        }

        [Test]
        public void TakeContract_NpcIsNotOccupied_NpcIsOccupiedTrue()
        {
            _npc.TakeContract();
            Assert.IsTrue(_npc.IsOccupied);
        }

        [Test]
        public void TakeContract_NpcIsOccupied_InvalidOperationException()
        {
            _npc.TakeContract();
            Assert.Throws<InvalidOperationException>(() => _npc.TakeContract());
        }
    }
}
