using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IHoleRepository
    {
        IQueryable<Hole> GetAll();
        Hole Get(int id);
        Hole Add(Hole item);
        void Remove(int id);
        bool Update(Hole item);
        string Metadata();
    }
}