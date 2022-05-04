using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IScenarioCreatorService
    {
        EventDto GetModel();

        void Accept();

        void Skip();

        void UseEnteredFee(decimal fee);

        void Reset();
    }
}
