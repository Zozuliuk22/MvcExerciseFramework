using BLL.Events;

namespace BLL.Interfaces
{
    public interface IMeetingService
    {
        Meeting CreateRandomGuildMeeting();

        void Reset();
    }
}
