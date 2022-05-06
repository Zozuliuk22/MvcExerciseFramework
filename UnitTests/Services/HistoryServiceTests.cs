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
    public class HistoryServiceTests
    {
        private HistoryService _historyService;
        private Mock<EventHistoryDto> _eventHistoryDto;

        [SetUp]
        public void SetUp()
        {
            _historyService = new HistoryService();
            _eventHistoryDto = new Mock<EventHistoryDto>();
        }

        [Test]
        public void Add_NewEventHistoryDto_CountOfEventsHistoryPlusOne()
        {
            var startCount = _historyService.EventsHistory.Count();
            _historyService.Add(_eventHistoryDto.Object);
            Assert.AreEqual(startCount + 1, _historyService.EventsHistory.Count());
        }

        [Test]
        [TestCase(null)]
        public void Add_NewEventHistoryDto_CountOfEventsHistoryPlusOne(EventHistoryDto eventHistory)
        {
            Assert.Throws<ArgumentNullException>(() => _historyService.Add(eventHistory));
        }
    }
}
