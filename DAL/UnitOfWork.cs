using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private AnkhMorporkContext _context;

        public IRepository<AssassinNpc> _assassinNpcs;

        public IRepository<BeggarNpc> _beggarNpcs;

        public IRepository<FoolNpc> _foolNpcs;

        public IPlayerRepository _players;

        public UnitOfWork(AnkhMorporkContext context)
        {
            _context = context;

            _assassinNpcs = new Repository<AssassinNpc>(context);
            _beggarNpcs = new Repository<BeggarNpc>(context);
            _foolNpcs = new Repository<FoolNpc>(context);
            _players = new PlayerRepository(context);
        }


        public IRepository<AssassinNpc> AssassinNpcs
        {
            get => _assassinNpcs is null ? _assassinNpcs : new Repository<AssassinNpc>(_context);
        }
            

        public IRepository<BeggarNpc> BeggarNpcs
        {
            get => _beggarNpcs is null ? _beggarNpcs : new Repository<BeggarNpc>(_context);
        }
            

        public IRepository<FoolNpc> FoolNpcs
        {
            get => _foolNpcs is null ? _foolNpcs : new Repository<FoolNpc>(_context);
        }

        public IPlayerRepository PlayerRepository
        {
            get => _players is null ? _players : new PlayerRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
