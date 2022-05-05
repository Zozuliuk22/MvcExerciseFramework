using System;
using System.Collections.Generic;
using BLL.NPCs;
using BLL.Constants;
using DAL.Interfaces;
using System.Drawing;
using BLL.Properties;

namespace BLL.Guilds
{
    public class FoolsGuild : Guild
    {
        private IUnitOfWork _unitOfWork;

        private List<FoolNpc> _npcs = new List<FoolNpc>();
        private FoolNpc _activeNpc;

        public FoolsGuild(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeGuild();
        }

        public override string WelcomeMessage
        {
            get => NpcsResources.FoolsWelcomeMessage;
        }

        public override string GuildColor => Colors.FoolsColor;

        public override Bitmap GuildImage => Images.FoolsGuild;

        private void InitializeGuild()
        {
            var npcs = _unitOfWork.FoolNpcs.GetAll();

            foreach (var npc in npcs)
            {
                _npcs.Add(new FoolNpc()
                {
                    Name = npc.Name,
                    Practice = npc.Practice,
                    Bonus = PracticesInfo.FoolsPracticeInfo[npc.Practice].Item2,
                    FullPracticeName = PracticesInfo.FoolsPracticeInfo[npc.Practice].Item1
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

        public override string PlayGame(PlayerLogic player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            player.EarnMoney(_activeNpc.Bonus);
            return NpcsResources.FoolsPlayGamePrescript + $" {_activeNpc.FullPracticeName}.";
        }

        public override string LoseGame(PlayerLogic player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            player.HasIneffectualMeeting();

            return NpcsResources.FoolsLoseGame;
        }

        public override void Reset()
        {
            _activeNpc = null;
        }

        public override string ToString() => NpcsResources.FoolsGuild;     
    }
}

