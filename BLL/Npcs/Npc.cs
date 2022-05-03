using System;
using BLL.Properties;

namespace BLL.NPCs
{
    public abstract class Npc
    {
        public string Name { get; set; }

        protected Npc() => Name = NpcsResources.UnknownName;

        protected Npc(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException($"The NPC's name must consist of symbols.");

            Name = name;
        }

        public override string ToString() => Name;
    }
}

