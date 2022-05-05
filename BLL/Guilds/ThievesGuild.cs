using System;
using System.Drawing;
using BLL.Properties;

namespace BLL.Guilds
{
    public class ThievesGuild : Guild
    {
        private const int _maxNumberThefts = 6;
        private const decimal _defaultFee = 10;

        public override string WelcomeMessage
        {
            get => NpcsResources.ThievesWelcomeMessage;
        }

        public override string GuildColor => Colors.ThievesColor;

        public override Bitmap GuildImage => Images.ThievesGuild;

        public int MaxNumberThefts => _maxNumberThefts;

        public decimal DefaultFee => _defaultFee;

        public int CurrentNumberThefts { get; private set; } = 0;

        public void AddTheft() => CurrentNumberThefts += 1;

        public override string PlayGame(PlayerLogic player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            if (player.CurrentBudget >= DefaultFee)
            {
                player.LoseMoney(DefaultFee);
                return $"Unknown from the {ToString()} stole {DefaultFee} AM$ from you.";
            }
            else
                return LoseGame(player) + " " + NpcsResources.ThievesLoseGamePostscript;
        }

        public override string LoseGame(PlayerLogic player)
        {
            return base.LoseGame(player) + " " + NpcsResources.ThiefKillsPlayerPostscript;
        }

        public override void Reset()
        {
            CurrentNumberThefts = 0;
        }

        public override string ToString() => NpcsResources.ThievesGuild;
    }
}

