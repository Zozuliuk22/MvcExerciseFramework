using System;
using System.Collections.Generic;
using DAL.Enums;
using DAL.Entities;

namespace DAL.Data
{
    internal static class DataSets
    {
        private static Random _random = new Random();

        internal static readonly List<AssassinNpc> Assassins = new List<AssassinNpc>()
        {
            new AssassinNpc()
                {
                    Name = "Black Widow",
                    MinReward = _random.Next(1,16),
                    MaxReward = _random.Next(16,31)
                },
            new AssassinNpc()
                {
                    Name = "Mockingjay",
                    MinReward = _random.Next(1, 16),
                    MaxReward = _random.Next(16, 31)
                },
            new AssassinNpc()
                {
                    Name = "Lonely Barman",
                    MinReward = _random.Next(1, 16),
                    MaxReward = _random.Next(16, 31)
                },
            new AssassinNpc()
                {
                    Name = "Robot Arlye",
                    MinReward = _random.Next(1, 16),
                    MaxReward = _random.Next(16, 31)
                },
            new AssassinNpc()
                {
                    Name = "Sniper Ghost",
                    MinReward = _random.Next(1,16),
                    MaxReward = _random.Next(16,31)
                },
        };

        internal static readonly List<BeggarNpc> Beggars = new List<BeggarNpc>()
        {
            new BeggarNpc()
            {
                Name = "John",
                Practice = BeggarsPractice.Twitchers
            },
            new BeggarNpc()
            {
                Name = "Elleon",
                Practice = BeggarsPractice.Droolers
            },
            new BeggarNpc()
            {
                Name = "Bobby",
                Practice = BeggarsPractice.Dribblers
            },
            new BeggarNpc()
            {
                Name = "George",
                Practice = BeggarsPractice.Mumblers
            },
            new BeggarNpc()
            {
                Name = "Shyam",
                Practice = BeggarsPractice.Mutterers
            },
            new BeggarNpc()
            {
                Name = "Ronnie",
                Practice = BeggarsPractice.WalkingAlongShouters
            },
            new BeggarNpc()
            {
                Name = "Katerina",
                Practice = BeggarsPractice.Demanders
            },
            new BeggarNpc()
            {
                Name = "Tyron",
                Practice = BeggarsPractice.JimmyCaller
            },
            new BeggarNpc()
            {
                Name = "Onur",
                Practice = BeggarsPractice.EightpenceForMeal
            },
            new BeggarNpc()
            {
                Name = "Nadia",
                Practice = BeggarsPractice.TwopenceForTea
            },
            new BeggarNpc()
            {
                Name = "Mohsin",
                Practice = BeggarsPractice.BeerNeeders
            },
        };

        internal static readonly List<FoolNpc> Fools = new List<FoolNpc>()
        {
            new FoolNpc
            {
                Name = "Dillon",
                Practice = FoolsPractice.Muggins
            },
            new FoolNpc
            {
                Name = "Zoey",
                Practice = FoolsPractice.Gull
            },
            new FoolNpc
            {
                Name = "Humphrey",
                Practice = FoolsPractice.Dupe
            },
            new FoolNpc
            {
                Name = "Stephan",
                Practice = FoolsPractice.Butt
            },
            new FoolNpc
            {
                Name = "Chloe-Louise",
                Practice = FoolsPractice.Fool
            },
            new FoolNpc
            {
                Name = "Montel",
                Practice = FoolsPractice.Tomfool
            },
            new FoolNpc
            {
                Name = "Lynn",
                Practice = FoolsPractice.StupidFool
            },
            new FoolNpc
            {
                Name = "Iylah",
                Practice = FoolsPractice.ArchFool
            },
            new FoolNpc
            {
                Name = "Jozef",
                Practice = FoolsPractice.CompleteFool
            },
        };
    }
}
