using System;
using BLL.Guilds;
using BLL.NPCs;

namespace BLL
{
    public class Meeting
    {
        public Guild Guild { get; private set; }

        public Npc Npc { get; private set; }

        public Meeting(Guild guild)
        {
            if (guild is null)
                throw new ArgumentNullException("The guild value cannot be null.");

            Guild = guild;
        }

        public Meeting(Guild guild, Npc npc)
        {
            if (guild is null)
                throw new ArgumentNullException("The guild value cannot be null.");

            Guild = guild;

            if (npc is null)
                throw new ArgumentNullException("The NPC value cannot be null.");

            Npc = npc;
        }

        public override string ToString()
        {
            return $"You have a meeting with the {Guild}.";
        }
    }
}
