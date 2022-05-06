using Moq;
using NUnit.Framework;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using DAL;
using DAL.Interfaces;
using BLL.Guilds;
using BLL;

namespace UnitTests
{
    [TestFixture]
    public class AssassinsGuildTests
    {
        private AnkhMorporkContext _context;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IRepository<DAL.Entities.AssassinNpc>> _repository;
        private PlayerLogic _player;
        private AssassinsGuild _assassinsGuild;

        [SetUp]
        public void SetUp()
        {
            var fakeContext = new Mock<AnkhMorporkContext>();
            fakeContext.Object.Assassin = SetUpAssassins<DAL.Entities.AssassinNpc>().Object;
            _context = fakeContext.Object;

            _unitOfWork = new Mock<IUnitOfWork>();
            _repository = new Mock<IRepository<DAL.Entities.AssassinNpc>>();
            _unitOfWork.Setup(uow => uow.AssassinNpcs).Returns(_repository.Object);
            _unitOfWork.Setup(uow => uow.AssassinNpcs.GetAll()).Returns(_context.Assassin);
            _player = new PlayerLogic("Name");
            _assassinsGuild = new AssassinsGuild(_unitOfWork.Object);
        }

        [Test]
        [TestCase(12)]
        public void CheckContract_FeeForAssassin2_ReturnActiveNpcNotNull(decimal fee)
        {
            var assassainsGuild = new AssassinsGuild(_unitOfWork.Object);
            assassainsGuild.CheckContract(fee);
            var assassin = assassainsGuild.GetActiveNpc();
            Assert.IsNotNull(assassin);
        }

        [Test]
        [TestCase(12)]
        public void CheckContract_FeeForAssassin2_ReturnActiveNpcAsAssassin2(decimal fee)
        {
            var assassainsGuild = new AssassinsGuild(_unitOfWork.Object);
            assassainsGuild.CheckContract(fee);
            var assassin = assassainsGuild.GetActiveNpc();
            Assert.AreEqual(assassin.Name, "Assassin2");
        }

        [Test]
        [TestCase(200)]
        public void CheckContract_FeeIsOutOfRange_ReturnFalse(decimal fee)
        {        
            var exist = _assassinsGuild.CheckContract(fee);
            Assert.IsFalse(exist);
        }

        [Test]
        [TestCase(-10)]
        public void CheckContract_FeeIsLessThanZero_ArgumentException(decimal fee)
        {
            Assert.Throws<ArgumentException>(() => _assassinsGuild.CheckContract(fee));
        }

        [Test]
        public void GetActiveNpc_WithoutCheckContact_ReturnNull()
        {
            var assassin = _assassinsGuild.GetActiveNpc();
            Assert.IsNull(assassin);
        }

        [Test]
        public void LoseGame_PlayerLoseGame_PlayerIsAliveFalse()
        {
            _assassinsGuild.LoseGame(_player);
            Assert.IsFalse(_player.IsAlive);
        }

        [Test]
        public void LoseGame_PlayerIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _assassinsGuild.LoseGame(null));
        }

        [Test]
        public void PlayGame_PlayerIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _assassinsGuild.PlayGame(null));
        }

        [Test]
        [TestCase(150)]
        public void PlayGame_ActiveNpcIsNull_PlayerIsAliveFalse(decimal fee)
        {
            _assassinsGuild.CheckContract(fee);
            _assassinsGuild.PlayGame(_player);
            Assert.IsFalse(_player.IsAlive);
        }

        [Test]
        [TestCase(12, 100)]
        public void PlayGame_PlayerWinGame_PlayerLoseMoney(decimal fee, decimal startBudget)
        {
            _assassinsGuild.CheckContract(fee);
            _assassinsGuild.PlayGame(_player);
            Assert.AreEqual(_player.CurrentBudget, startBudget-fee);
        }

        private Mock<DbSet<DAL.Entities.AssassinNpc>> SetUpAssassins<T>() where T : DAL.Entities.AssassinNpc
        {
            var sourceList = new List<DAL.Entities.AssassinNpc>
            {
                new DAL.Entities.AssassinNpc{
                    Name = "Assassin1",
                    MinReward = 1,
                    MaxReward = 10
                },
                new DAL.Entities.AssassinNpc{
                    Name = "Assassin2",
                    MinReward = 11,
                    MaxReward = 20
                }
            };
            var queryable = sourceList.AsQueryable();
            var assassinDbSet = new Mock<DbSet<DAL.Entities.AssassinNpc>>();
            assassinDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            assassinDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            assassinDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            assassinDbSet.As<IQueryable<DAL.Entities.AssassinNpc>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            assassinDbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            return assassinDbSet;
        }
    }
}
