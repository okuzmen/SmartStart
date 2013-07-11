using System.Collections.Generic;
using System.Linq;
using Breeze.WebApi;
using Newtonsoft.Json.Linq;
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
        SaveResult SaveChanges(JObject saveBundle);

        string Metadata();
    }
}