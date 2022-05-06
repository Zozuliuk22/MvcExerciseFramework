using System.Collections.Generic;
using System.Linq;
using System;
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
            if(playerDto is null)
                throw new ArgumentNullException(nameof(playerDto), "The playerDto value cannot be null.");
            
            var playerDb = new Player()
            {
                Name = playerDto.Name,
            };
            _unitOfWork.Players.Create(playerDb);
            _unitOfWork.Save();        
            
            playerDb = _unitOfWork.Players.GetAll().Last();
            SetCurrentPlayer(playerDb);
        }

        public void Update(PlayerLogic player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");
            
            var playerDb = _unitOfWork.Players.GetById(player.Id);
            playerDb.Name = player.Name;
            playerDb.HighScore = player.HighScore;
            _unitOfWork.Players.Update(playerDb);
            _unitOfWork.Save();
        }

        public PlayerDto GetById(int id)
        {
            var player = _unitOfWork.Players.GetById(id);
            return new PlayerDto()
            {
                Id = player.Id,
                Name = player.Name
            };
        }

        public IEnumerable<PlayerDto> GetAll()
        {
            var playersDb = _unitOfWork.Players.GetAll().ToList();
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
            if(playerDto is null)
                throw new ArgumentNullException(nameof(playerDto), "The player value cannot be null.");
            
            if (playerDto.Id == 0)
                Create(playerDto);
            else
            {
                var playerDb = _unitOfWork.Players.GetById(playerDto.Id);
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
