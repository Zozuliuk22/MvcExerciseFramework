using System.Collections.Generic;
using System;
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
            if(eventHistory is null)
                throw new ArgumentNullException(nameof(eventHistory), "Event of history cannot be null.");
            eventHistory.Id = _history.Count + 1;
            _history.Add(eventHistory);
        }

        public void Reset()
        {
            _history.Clear();
        }
    }
}
