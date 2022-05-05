using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using BLL.Dtos;
using DAL.Interfaces;

namespace BLL.Services
{
    public class PlayerService : IPlayerService
    {
        private IUnitOfWork _unitOfWork;

        public Player CurrentPlayer { get; set; }

        public PlayerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(PlayerDto playerDto)
        {
            var playerDb = new DAL.Entities.Player()
            {
                Name = playerDto.Name,
            };
            _unitOfWork.PlayerRepository.Create(playerDb);
            _unitOfWork.Save();        
            
            playerDto.Id = _unitOfWork.PlayerRepository.GetAll().Last().Id;
            SetCurrentPlayer(playerDto);
        }

        public void Update(Player player)
        {
            var playerDb = _unitOfWork.PlayerRepository.GetById(player.Id);
            playerDb.Name = player.Name;
            playerDb.HighScore = player.HighScore;
            _unitOfWork.PlayerRepository.Update(playerDb);
            _unitOfWork.Save();
        }

        public PlayerDto GetById(int id)
        {
            var player = _unitOfWork.PlayerRepository.GetById(id);
            return new PlayerDto()
            {
                Id = player.Id,
                Name = player.Name,
                HighScore = player.HighScore
            };
        }

        public IEnumerable<PlayerDto> GetAll()
        {
            var playersDb = _unitOfWork.PlayerRepository.GetAll().ToList();
            var playersReturn = new List<PlayerDto>();
            playersDb.ForEach(p => playersReturn.Add(new PlayerDto()
            {
                Id = p.Id,
                Name = p.Name,
                HighScore=p.HighScore
            }));
            return playersReturn;
        }

        public void SetPlayer(PlayerDto playerDto)
        {
            if (playerDto.Id == 0)
                Create(playerDto);
            else
               SetCurrentPlayer(playerDto);
        }

        private void SetCurrentPlayer(PlayerDto playerDto)
        {
            CurrentPlayer = new Player(playerDto.Name);
            CurrentPlayer.Id = playerDto.Id;
            CurrentPlayer.HighScore = playerDto.HighScore;
        }
    }
}
