using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Guilds;
using System.Reflection;
using DAL.Interfaces;
using BLL.DTOs;
using BLL.Properties;
using BLL.Interfaces;
using BLL.Events;

namespace BLL.Services
{
    public class ScenarioCreatorService : IScenarioCreatorService
    {
        private readonly IMeetingService _meetingService;

        private Player _currentPlayer;
        private Pub _pub;
        private Meeting _currentMeeting;

        private bool _isPub;
        private string _meetingResult;

        public ScenarioCreatorService(IMeetingService meetingService)
        {           
            _meetingService = meetingService;
            _pub = new Pub();   
            _currentPlayer = new Player("Viktor");
        }

        private void CreateRandomGuildMeetingOrBar()
        {
            if (_currentPlayer.CurrentBeers < _currentPlayer.MaxBeers 
                    && _currentPlayer.Score > _pub.AccessToPub)
            {
                _isPub = new Random().Next(2) == 0;
                _pub.AccessToPub += _pub.DefaultStep;
            }                
            else
                _isPub = false;

            _currentMeeting = _meetingService.CreateRandomGuildMeeting();
        }

        public EventDto GetModel()
        {
            CreateRandomGuildMeetingOrBar();

            var meetingDto = new EventDto();

            if (_isPub)
            {
                meetingDto.Name = _pub.ToString();
                meetingDto.WelcomeWord = _pub.WelcomeMessage;
                meetingDto.Color = _pub.Color.ToString();
                meetingDto.Image = _pub.Image;
                meetingDto.IsEnteringFee = false;
            }
            else
            {
                meetingDto.Name = _currentMeeting.ToString();
                meetingDto.WelcomeWord = _currentMeeting.Guild.WelcomeMessage;
                meetingDto.WelcomeWord += _currentMeeting.Npc != null ? _currentMeeting.Npc.ToString() : String.Empty;
                meetingDto.Color = _currentMeeting.Guild.GuildColor.ToString();
                meetingDto.Image = _currentMeeting.Guild.GuildImage;
                meetingDto.IsEnteringFee = _currentMeeting.Guild is AssassinsGuild;
            }

            meetingDto.IsPub = _isPub;
            meetingDto.PlayerScore = _currentPlayer.ToString();
            meetingDto.PlayerIsAlive = _currentPlayer.IsAlive;
            meetingDto.PlayerCurrentBudget = _currentPlayer.CurrentBudget;
            meetingDto.PlayerCurrentBeers = _currentPlayer.CurrentBeers;
            meetingDto.ResultMeetingMessage = _meetingResult;

            return meetingDto;
        }        

        public void Accept()
        {
            if (_isPub)
                _meetingResult = _pub.PlayGame(_currentPlayer);
            _meetingResult = _currentMeeting.Guild.PlayGame(_currentPlayer);
        }

        public void Skip()
        {
            if (_isPub)
                _meetingResult = _pub.LoseGame();
            _meetingResult = _currentMeeting.Guild.LoseGame(_currentPlayer);

        }

        public void UseEnteredFee(decimal fee)
        {
            if (_currentMeeting.Guild is AssassinsGuild)
            {
                if (((AssassinsGuild)_currentMeeting.Guild).CheckContract(fee))
                    ((AssassinsGuild)_currentMeeting.Guild).GetActiveNpc();
            }
        }

        public void Reset()
        {
            _meetingService.Reset();
            _pub.Reset();
            _currentPlayer.Reset();
            _meetingResult = String.Empty;
        }
    }
}

