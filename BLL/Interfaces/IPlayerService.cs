using System.Collections.Generic;
using BLL.Dtos;

namespace BLL.Interfaces
{
    public interface IPlayerService
    {
        Player CurrentPlayer { get; set; }

        void Create(PlayerDto playerDto);

        void Update(Player player);

        PlayerDto GetById(int id);

        IEnumerable<PlayerDto> GetAll();

        void SetPlayer(PlayerDto playerDto);
    }
}
