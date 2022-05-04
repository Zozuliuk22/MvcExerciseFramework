using System;
using System.Collections.Generic;
using BLL.NPCs;
using BLL.Constants;
using DAL.Enums;
using DAL.Interfaces;
using System.Drawing;
using BLL.Properties;

namespace BLL.Guilds
{
    public class BeggarsGuild : Guild
    {
        private IUnitOfWork _unitOfWork;

        private List<BeggarNpc> _npcs = new List<BeggarNpc>();
        private BeggarNpc _activeNpc;

        public BeggarsGuild(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeGuild();
        }

        public override string WelcomeMessage
        {
            get => NpcsResources.BeggarsWelcomeMessage;
        }

        public override string GuildColor => Colors.BeggarsColor;

        public override Bitmap GuildImage => Images.BeggarsGuild;

        private void InitializeGuild()
        {
            var npcs = _unitOfWork.BeggarNpcs.GetAll();

            foreach (var npc in npcs)
            {
                _npcs.Add(new BeggarNpc()
                {
                    Name = npc.Name,
                    Practice = npc.Practice,
                    Fee = PracticesInfo.BeggarsPracticeInfo[npc.Practice].Item2,
                    FullPracticeName = PracticesInfo.BeggarsPracticeInfo[npc.Practice].Item1
                });
            }
        }

        public Npc GetActiveNpc()
        {
            if (!_npcs.Equals(null) && _npcs.Count > 0)
                _activeNpc = _npcs[new Random().Next(0, _npcs.Count)];
            else
                throw new ArgumentNullException("No one NPC was created.");

            return _activeNpc;
        }

        public override string PlayGame(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            if (_activeNpc.Practice.Equals(BeggarsPractice.BeerNeeders))
            {
                return player.CurrentBeers > 0 ? 
                    player.LoseBeer() + " " + NpcsResources.PlayerIsNotLackOfBeerPostscript :
                    player.ToDie() + " " + NpcsResources.PlayerIsLackOfBeerPostscript;
            }
            else if (player.CurrentBudget >= _activeNpc.Fee)
            {
                player.LoseMoney(_activeNpc.Fee);
                return NpcsResources.PlayerWinsBeggar;
            }
            else
                return player.ToDie() + " " + NpcsResources.PlayerIsLackOfMoneyPostscript +
                    $" And {_activeNpc.Name} chased you to death.";
        }

        public override string LoseGame(Player player)
        {
            return base.LoseGame(player) + " " + NpcsResources.BeggarKillsPlayerPostscript;
        }

        public override void Reset()
        {
            _activeNpc = null;
        }

        public override string ToString() => NpcsResources.BeggarsGuild;     
    }
}

