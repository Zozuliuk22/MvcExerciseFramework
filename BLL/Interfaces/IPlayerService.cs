using System.Collections.Generic;
using BLL.Dtos;

namespace BLL.Interfaces
{
    public interface IPlayerService
    {
        PlayerLogic CurrentPlayer { get; set; }

        void Create(PlayerDto playerDto);

        void Update(PlayerLogic player);

        PlayerDto GetById(int id);

        IEnumerable<PlayerDto> GetAll();

        void SetPlayer(PlayerDto playerDto);
    }
}
