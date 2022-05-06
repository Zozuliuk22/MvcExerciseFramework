using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
    }
}

