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
    public class BeggarsGuildTests
    {
        private AnkhMorporkContext _context;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IRepository<DAL.Entities.BeggarNpc>> _repository;
        private PlayerLogic _player;
        private BeggarsGuild _beggarsGuild;

        [SetUp]
        public void SetUp()
        {
            var fakeContext = new Mock<AnkhMorporkContext>();
            fakeContext.Object.Beggar = SetUpBeggars<DAL.Entities.BeggarNpc>().Object;
            _context = fakeContext.Object;

            _unitOfWork = new Mock<IUnitOfWork>();
            _repository = new Mock<IRepository<DAL.Entities.BeggarNpc>>();
            _unitOfWork.Setup(uow => uow.BeggarNpcs).Returns(_repository.Object);
            _unitOfWork.Setup(uow => uow.BeggarNpcs.GetAll()).Returns(_context.Beggar);
            _player = new PlayerLogic("Name");
            _beggarsGuild = new BeggarsGuild(_unitOfWork.Object);
        }

        [Test]
        public void GetActiveNpc_NpcsInitialized_ReturnNotNull()
        {
            var beggar = _beggarsGuild.GetActiveNpc();
            Assert.IsNotNull(beggar);
        }

        [Test]
        public void GetActiveNpc_NpcsNotInitialized_ArgumentNullException()
        {            
            _unitOfWork.Setup(uow => uow.BeggarNpcs.GetAll()).Returns(new List<DAL.Entities.BeggarNpc>());
            _beggarsGuild = new BeggarsGuild(_unitOfWork.Object);
            Assert.Throws<ArgumentNullException>(() => _beggarsGuild.GetActiveNpc());
        }

        [Test]
        public void LoseGame_PlayerLoseGame_PlayerIsAliveFalse()
        {
            _beggarsGuild.LoseGame(_player);
            Assert.IsFalse(_player.IsAlive);
        }

        [Test]
        public void LoseGame_PlayerIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _beggarsGuild.LoseGame(null));
        }

        [Test]
        public void PlayGame_PlayerIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _beggarsGuild.PlayGame(null));
        }

        [Test]
        [TestCase(100)]
        public void PlayGame_PlayerWinGame_PlayerLoseMoney(decimal startBudget)
        {
            var beggar = (BeggarNpc)_beggarsGuild.GetActiveNpc();
            _beggarsGuild.PlayGame(_player);
            Assert.AreEqual(_player.CurrentBudget, startBudget - beggar.Fee);
        }

        [Test]
        [TestCase(100)]
        public void PlayGame_PlayerDoesNotHaveEnoughMoney_PlayerIsAliveFalse(decimal startBudget)
        {
            _beggarsGuild.GetActiveNpc();
            _player.LoseMoney(startBudget);
            _beggarsGuild.PlayGame(_player);
            Assert.IsFalse(_player.IsAlive);
        }

        [Test]
        public void PlayGame_PlayerMetBeerNeederWithoutBeer_PlayerIsAliveFalse()
        {
            var beggarDb = new DAL.Entities.BeggarNpc()
            {
                Name = "BeerNeederBeggar",
                Practice = BeggarsPractice.BeerNeeders
            };
            _unitOfWork.Setup(uow => uow.BeggarNpcs.GetAll()).Returns(new List<DAL.Entities.BeggarNpc>() { beggarDb });
            _beggarsGuild = new BeggarsGuild(_unitOfWork.Object);
            _beggarsGuild.GetActiveNpc();
            _beggarsGuild.PlayGame(_player);
            Assert.IsFalse(_player.IsAlive);
        }

        [Test]
        public void PlayGame_PlayerMetBeerNeederWithBeer_PlayerIsAliveTrue()
        {
            var beggarDb = new DAL.Entities.BeggarNpc()
            {
                Name = "BeerNeederBeggar",
                Practice = BeggarsPractice.BeerNeeders
            };
            _unitOfWork.Setup(uow => uow.BeggarNpcs.GetAll()).Returns(new List<DAL.Entities.BeggarNpc>() { beggarDb });
            _beggarsGuild = new BeggarsGuild(_unitOfWork.Object);
            _beggarsGuild.GetActiveNpc();
            _player.BuyBeer();
            _beggarsGuild.PlayGame(_player);
            Assert.IsTrue(_player.IsAlive);
        }


        private Mock<DbSet<DAL.Entities.BeggarNpc>> SetUpBeggars<T>() where T : DAL.Entities.BeggarNpc
        {
            var sourceList = new List<DAL.Entities.BeggarNpc>
            {
                new DAL.Entities.BeggarNpc{
                    Name = "Beggar1",
                    Practice = BeggarsPractice.Twitchers
                }
            };
            var queryable = sourceList.AsQueryable();
            var beggarDbSet = new Mock<DbSet<DAL.Entities.BeggarNpc>>();
            beggarDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            beggarDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            beggarDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            beggarDbSet.As<IQueryable<DAL.Entities.BeggarNpc>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            beggarDbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            return beggarDbSet;
        }
    }
}

