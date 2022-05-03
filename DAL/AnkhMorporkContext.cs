using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
    public class AnkhMorporkContext : DbContext
    {
        public DbSet<AssassinNpc> Assassin { get; set; }

        public DbSet<BeggarNpc> Beggar { get; set; }

        public DbSet<FoolNpc> Fool { get; set; }

        public DbSet<Player> Player { get; set; }

        public AnkhMorporkContext() : base("AnkhMorporkContext") { }
    }
}
