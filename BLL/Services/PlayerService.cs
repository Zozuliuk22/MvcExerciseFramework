using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using BLL.Dtos;
using DAL.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    public class PlayerService : IPlayerService
    {
        private IUnitOfWork _unitOfWork;

        public PlayerLogic CurrentPlayer { get; set; }

        public PlayerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(PlayerDto playerDto)
        {
            var playerDb = new Player()
            {
                Name = playerDto.Name,
            };
            _unitOfWork.PlayerRepository.Create(playerDb);
            _unitOfWork.Save();        
            
            playerDb = _unitOfWork.PlayerRepository.GetAll().Last();
            SetCurrentPlayer(playerDb);
        }

        public void Update(PlayerLogic player)
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
                Name = player.Name
            };
        }

        public IEnumerable<PlayerDto> GetAll()
        {
            var playersDb = _unitOfWork.PlayerRepository.GetAll().ToList();
            var playersReturn = new List<PlayerDto>();
            playersDb.ForEach(p => playersReturn.Add(new PlayerDto()
            {
                Id = p.Id,
                Name = p.Name
            }));
            return playersReturn;
        }

        public void SetPlayer(PlayerDto playerDto)
        {
            if (playerDto.Id == 0)
                Create(playerDto);
            else
            {
                var playerDb = _unitOfWork.PlayerRepository.GetById(playerDto.Id);
                SetCurrentPlayer(playerDb);
            }
               
        }

        private void SetCurrentPlayer(Player playerDb)
        {
            CurrentPlayer = new PlayerLogic(playerDb.Name);
            CurrentPlayer.Id = playerDb.Id;
            CurrentPlayer.HighScore = playerDb.HighScore;
        }
    }
}
