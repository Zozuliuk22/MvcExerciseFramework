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
    public class FoolsGuildTests
    {
        private AnkhMorporkContext _context;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IRepository<DAL.Entities.FoolNpc>> _repository;
        private PlayerLogic _player;
        private FoolsGuild _foolsGuild;

        [SetUp]
        public void SetUp()
        {
            var fakeContext = new Mock<AnkhMorporkContext>();
            fakeContext.Object.Fool = SetUpFools<DAL.Entities.FoolNpc>().Object;
            _context = fakeContext.Object;

            _unitOfWork = new Mock<IUnitOfWork>();
            _repository = new Mock<IRepository<DAL.Entities.FoolNpc>>();
            _unitOfWork.Setup(uow => uow.FoolNpcs).Returns(_repository.Object);
            _unitOfWork.Setup(uow => uow.FoolNpcs.GetAll()).Returns(_context.Fool);
            _player = new PlayerLogic("Name");
            _foolsGuild = new FoolsGuild(_unitOfWork.Object);
        }

        [Test]
        public void GetActiveNpc_NpcsInitialized_ReturnNotNull()
        {
            var fool = _foolsGuild.GetActiveNpc();
            Assert.IsNotNull(fool);
        }

        [Test]
        public void GetActiveNpc_NpcsNotInitialized_ArgumentNullException()
        {
            _unitOfWork.Setup(uow => uow.FoolNpcs.GetAll()).Returns(new List<DAL.Entities.FoolNpc>());
            _foolsGuild = new FoolsGuild(_unitOfWork.Object);
            Assert.Throws<ArgumentNullException>(() => _foolsGuild.GetActiveNpc());
        }

        [Test]
        public void LoseGame_PlayerIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _foolsGuild.LoseGame(null));
        }

        [Test]
        public void PlayGame_PlayerIsNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _foolsGuild.PlayGame(null));
        }

        [Test]
        [TestCase(100)]
        public void PlayGame_PlayerWinGame_PlayerEarnMoney(decimal startBudget)
        {
            var fool = (FoolNpc)_foolsGuild.GetActiveNpc();
            _foolsGuild.PlayGame(_player);
            Assert.AreEqual(_player.CurrentBudget, startBudget + fool.Bonus);
        }



        private Mock<DbSet<DAL.Entities.FoolNpc>> SetUpFools<T>() where T : DAL.Entities.FoolNpc
        {
            var sourceList = new List<DAL.Entities.FoolNpc>
            {
                new DAL.Entities.FoolNpc{
                    Name = "Fool1",
                    Practice = FoolsPractice.Tomfool
                }
            };
            var queryable = sourceList.AsQueryable();
            var foolDbSet = new Mock<DbSet<DAL.Entities.FoolNpc>>();
            foolDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            foolDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            foolDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            foolDbSet.As<IQueryable<DAL.Entities.FoolNpc>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            foolDbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            return foolDbSet;
        }
    }
}


