﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BLL.NPCs;
using BLL.Properties;
using DAL.Interfaces;

namespace BLL.Guilds
{
    public class AssassinsGuild : Guild
    {
        private IUnitOfWork _unitOfWork;

        private List<AssassinNpc> _npcs = new List<AssassinNpc>();
        private AssassinNpc _activeNpc;
        private decimal _enteredFee;        

        public AssassinsGuild(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeGuild();
        }

        public override string WelcomeMessage
        {
            get => NpcsResources.AssassinsWelcomeMessage;
        }

        public override string GuildColor => Colors.AssassinsColor;

        public override Bitmap GuildImage => Images.AssassinsGuild;

        private void InitializeGuild()
        {
            var npcs = _unitOfWork.AssassinNpcs.GetAll();

            foreach (var npc in npcs)
            {
                _npcs.Add(new AssassinNpc()
                {
                    Name = npc.Name,
                    MinReward = npc.MinReward,
                    MaxReward = npc.MaxReward
                });
            }
        }        

        public bool CheckContract(decimal fee)
        {
            if (fee > 0)
            {
                _activeNpc = _npcs.OfType<AssassinNpc>()
                           .Where(v => v.IsOccupied.Equals(false))
                           .Where(v => v.MinReward <= fee && v.MaxReward >= fee)
                           .OrderBy(v => v.Name)
                           .FirstOrDefault();
            }
            else
                throw new ArgumentException("The entered fee must be bigger than zero.");

            if (_activeNpc is null) return false;

            _enteredFee = fee;

            return true;
        }

        public Npc GetActiveNpc()
        {
            if (_activeNpc is null) return null;
            if (_activeNpc.IsOccupied)
                throw new InvalidOperationException("Before this, player must enter fee and check contract.");
            return _activeNpc;
        }

        public override string PlayGame(PlayerLogic player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            if (_activeNpc is null)
                return player.ToDie() + " " + NpcsResources.AssassinKillsPlayerPostscript;
            else
            {
                _activeNpc.TakeContract();
                player.LoseMoney(_enteredFee);
                return $"You are lucky! Assassin {_activeNpc} went to fulfill the contract.";
            }
        }

        public override string LoseGame(PlayerLogic player)
        {
            return base.LoseGame(player) + " " + NpcsResources.AssassinsLoseGamePostscript;
        }

        public override void Reset()
        {
            _activeNpc = null;
            _enteredFee = 0;
            _npcs.ForEach(a => a.CompliteContract());
        }

        public override string ToString() => NpcsResources.AssassinsGuild;        
    }
}

