using System;
using BLL.Properties;
using System.Drawing;

namespace BLL.Guilds
{
    public abstract class Guild
    {
        public virtual string WelcomeMessage => String.Empty;

        public virtual string GuildColor => Colors.Black;

        public virtual Bitmap GuildImage => Images.Default;

        public abstract string PlayGame(Player player);

        public virtual string LoseGame(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            return player.ToDie();
        }

        public abstract void Reset();
    }
}

