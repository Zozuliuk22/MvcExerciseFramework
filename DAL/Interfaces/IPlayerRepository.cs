using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Player GetById(int id);

        void Create(Player player);

        void Update(Player player);

        void DeleteById(int id);
    }
}
