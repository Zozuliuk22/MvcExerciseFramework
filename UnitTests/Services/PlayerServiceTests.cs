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
using BLL.Services;
using BLL.Dtos;

namespace UnitTests
{
    [TestFixture]
    public class PlayerServiceTests
    {
        private AnkhMorporkContext _context;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IPlayerRepository> _repository;

        private PlayerService _playerService;

        [SetUp]
        public void SetUp()
        {
            var fakeContext = new Mock<AnkhMorporkContext>();
            fakeContext.Object.Player = SetUpPlayers<DAL.Entities.Player>().Object;
            _context = fakeContext.Object;
            _unitOfWork = new Mock<IUnitOfWork>();
            _repository = new Mock<IPlayerRepository>();
            _unitOfWork.Setup(uow => uow.Players).Returns(_repository.Object);
            _unitOfWork.Setup(uow => uow.Players.GetAll()).Returns(_context.Player);            
            _unitOfWork.Setup(uow => uow.Players.GetById(It.IsInRange<int>(Int32.MinValue, -1, Range.Inclusive)))
                                                .Throws<ArgumentOutOfRangeException>();
            _unitOfWork.Setup(uow => uow.Players.GetById(It.IsInRange<int>(_context.Player.Count(), Int32.MaxValue, Range.Exclusive)))
                                               .Throws<ArgumentOutOfRangeException>(); 
            _unitOfWork.Setup(uow => uow.Save()).Callback(() => _context.SaveChanges());
            _playerService = new PlayerService(_unitOfWork.Object);
        }

        [Test]
        public void GetAll_DbContextPlayerIsNotEmpty_AmountOfPlayersEqualsDbSetPlayerCount()
        {
            var list = _playerService.GetAll();
            Assert.AreEqual(list.Count(), _context.Player.Count());
        }

        [Test]
        public void GetAll_DbContextPlayerIsEmpty_AmountOfPlayersEqualsNotNull()
        {
            _unitOfWork.Setup(uow => uow.Players.GetAll()).Returns(new List<DAL.Entities.Player>());
            var list = _playerService.GetAll();
            Assert.IsNotNull(list);
        }

        [Test]
        [TestCase(10)]
        [TestCase(-5)]
        public void GetById_IdIsOutOfRange_ArgumentOutOfRangeException(int id)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _playerService.GetById(id));
        }

        [Test]
        public void Create_ArgumentPlayerDtoIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _playerService.Create(null));
        }

        [Test]
        public void Update_ArgumentPlayerDtoIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _playerService.Update(null));
        }

        [Test]
        public void SetPlayer_ArgumentPlayerDtoIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _playerService.SetPlayer(null));
        }

        private Mock<DbSet<DAL.Entities.Player>> SetUpPlayers<T>() where T : DAL.Entities.Player
        {
            var sourceList = new List<DAL.Entities.Player>
            {
                new DAL.Entities.Player{
                    Name = "Player1",
                    HighScore = 12
                },
                new DAL.Entities.Player{
                    Name = "Player2",
                    HighScore = 31
                }
            };
            var queryable = sourceList.AsQueryable();
            var playerDbSet = new Mock<DbSet<DAL.Entities.Player>>();
            playerDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            playerDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            playerDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            playerDbSet.As<IQueryable<DAL.Entities.Player>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            playerDbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            return playerDbSet;
        }
    }
}
