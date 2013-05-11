using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IHoleRepository
    {
        IEnumerable<Hole> GetAll();
        Hole Get(int id);
        Hole Add(Hole item);
        void Remove(int id);
        bool Update(Hole item);
    }
}