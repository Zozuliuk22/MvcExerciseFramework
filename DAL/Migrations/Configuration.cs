namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DAL.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.AnkhMorporkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AnkhMorporkContext context)
        {
            DataSets.Assassins.ForEach(assassin => context.Assassin.AddOrUpdate(a => a.Name, assassin));
            DataSets.Beggars.ForEach(beggar => context.Beggar.AddOrUpdate(b => b.Name, beggar));
            DataSets.Fools.ForEach(fool => context.Fool.AddOrUpdate(f => f.Name, fool));
            context.SaveChanges();
        }
    }
}
