using System.Collections.Generic;
using BLL.Interfaces;
using BLL.Dtos;

namespace BLL.Services
{
    public class HistoryService : IHistoryService
    {
        private List<EventHistoryDto> _history;

        public List<EventHistoryDto> EventsHistory => _history;

        public HistoryService()
        {
            _history = new List<EventHistoryDto>();
        }

        public void Add(EventHistoryDto eventHistory)
        {
            eventHistory.Id = _history.Count + 1;
            _history.Add(eventHistory);
        }

        public void Reset()
        {
            _history.Clear();
        }
    }
}
