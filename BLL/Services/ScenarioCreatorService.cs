using System;
using BLL.Guilds;
using BLL.Dtos;
using BLL.Properties;
using BLL.Interfaces;
using BLL.Events;
using System.Windows.Forms;

namespace BLL.Services
{
    public class ScenarioCreatorService : IScenarioCreatorService
    {
        private readonly IMeetingService _meetingService;
        private readonly IHistoryService _historyService;
        private readonly IPlayerService _playerService;

        private Pub _pub;
        private Meeting _currentMeeting;

        private bool _isPub;
        private string _meetingResult;

        public ScenarioCreatorService(IMeetingService meetingService, 
                                      IHistoryService historyService,
                                      IPlayerService playerService)
        { 
            _historyService = historyService; 
            _meetingService = meetingService;
            _playerService = playerService;

            _pub = new Pub(); 

            CreateStartEventHistory();
        }

        private void CreateRandomGuildMeetingOrBar()
        {
            if (_playerService.CurrentPlayer.CurrentBeers < _playerService.CurrentPlayer.MaxBeers 
                    && _playerService.CurrentPlayer.Score > _pub.AccessToPub)
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
            meetingDto.PlayerScore = _playerService.CurrentPlayer.ToString();
            meetingDto.PlayerIsAlive = _playerService.CurrentPlayer.IsAlive;
            meetingDto.PlayerCurrentBudget = _playerService.CurrentPlayer.CurrentBudget;
            meetingDto.PlayerCurrentBeers = _playerService.CurrentPlayer.CurrentBeers;
            meetingDto.ResultMeetingMessage = _meetingResult;

            if (!_playerService.CurrentPlayer.IsAlive 
              && _playerService.CurrentPlayer.Score > _playerService.CurrentPlayer.HighScore)
            {
                _playerService.CurrentPlayer.HighScore = _playerService.CurrentPlayer.Score;
                _playerService.Update(_playerService.CurrentPlayer);
            }

            meetingDto.PlayerHighScore = _playerService.CurrentPlayer.HighScore;

            return meetingDto;
        }        

        public void Accept()
        {
            if (_isPub)
                _meetingResult = _pub.PlayGame(_playerService.CurrentPlayer);
            _meetingResult = _currentMeeting.Guild.PlayGame(_playerService.CurrentPlayer);
            CreateEventHistory();
        }

        public void Skip()
        {
            if (_isPub)
                _meetingResult = _pub.LoseGame();
            _meetingResult = _currentMeeting.Guild.LoseGame(_playerService.CurrentPlayer);
            CreateEventHistory();

        }

        private void CreateEventHistory()
        {
            var newEvent = new EventHistoryDto();
            newEvent.Name = _isPub ? _pub.ToString() : _currentMeeting.Guild.ToString();
            newEvent.Color = _isPub ? _pub.Color : _currentMeeting.Guild.GuildColor;
            newEvent.PlayerAlive = _playerService.CurrentPlayer.IsAlive ? DialogResult.Yes.ToString() : DialogResult.No.ToString();
            newEvent.Beers = _playerService.CurrentPlayer.CurrentBeers;
            newEvent.Budget = _playerService.CurrentPlayer.CurrentBudget;
            _historyService.Add(newEvent);
        }

        private void CreateStartEventHistory()
        {
            var newEvent = new EventHistoryDto();
            newEvent.Name = HistoryResources.StartEventHistoryName;
            newEvent.Color = Colors.Black;
            newEvent.PlayerAlive = _playerService.CurrentPlayer.IsAlive ? DialogResult.Yes.ToString() : DialogResult.No.ToString();
            newEvent.Beers = _playerService.CurrentPlayer.CurrentBeers;
            newEvent.Budget = _playerService.CurrentPlayer.CurrentBudget;
            _historyService.Add(newEvent);
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
            _historyService.Reset();            
            _playerService.CurrentPlayer.Reset();
            _pub.Reset();
            _meetingResult = String.Empty;
            CreateStartEventHistory();
        }
    }
}

