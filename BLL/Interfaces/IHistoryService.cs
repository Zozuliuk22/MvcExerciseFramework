using System.Collections.Generic;
using BLL.Dtos;

namespace BLL.Interfaces
{
    public interface IHistoryService
    {
        List<EventHistoryDto> EventsHistory { get; }

        void Add(EventHistoryDto eventHistory);

        void Reset();
    }
}
