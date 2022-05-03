using System;
using System.Linq;
using System.Data.Entity.Migrations;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        private readonly AnkhMorporkContext _context;

        public PlayerRepository(AnkhMorporkContext context) : base(context)
        {
            _context = context;
        }

        public void Create(Player player)
        {
            if(player is null)
                throw new ArgumentNullException(nameof(player), "The Player object cannot be null.");
            if(player.Name is null)
                throw new ArgumentNullException(nameof(player), "The Player's Name property cannot be null.");

            _context.Player.Add(player);
        }

        public void DeleteById(int id)
        {
            var item = _context.Player.FirstOrDefault(p => p.Id == id);
            if(item != null) _context.Player.Remove(item);
        }

        public Player GetById(int id)
        {
            return _context.Player.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The Player object cannot be null.");
            if (player.Name is null)
                throw new ArgumentNullException(nameof(player), "The Player's Name property cannot be null.");

            _context.Player.AddOrUpdate(p => p.Id, player);
        }
    }
}
