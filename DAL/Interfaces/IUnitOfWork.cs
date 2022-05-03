using System;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AssassinNpc> AssassinNpcs { get; }

        IRepository<BeggarNpc> BeggarNpcs { get; }

        IRepository<FoolNpc> FoolNpcs { get; }

        IPlayerRepository PlayerRepository { get; }

        int Save();
    }
}

