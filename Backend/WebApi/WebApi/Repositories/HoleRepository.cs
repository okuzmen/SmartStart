using System;
using System.Linq;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class HoleRepository : IHoleRepository
    {
        private readonly ContextProvider contextProvider = new ContextProvider();
        private int nextId = 1;


        public IQueryable<Hole> GetAll()
        {
            return contextProvider.Context.Holes;
        }

        public Hole Get(int id)
        {
            return contextProvider.Context.Holes.FirstOrDefault(h => h.Id == id);
        }

        public Hole Add(Hole item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = nextId++;
            contextProvider.Context.Holes.Add(item);
            contextProvider.Context.SaveChanges();
            return item;
        }

        public void Remove(int id)
        {
            var toRemove = contextProvider.Context.Holes.Where(h => h.Id == id);
            foreach (var hole in toRemove)
            {
                contextProvider.Context.Holes.Remove(hole);
            }
            contextProvider.Context.SaveChanges();
        }

        public bool Update(Hole item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var toUpdate = contextProvider.Context.Holes.SingleOrDefault(h => h.Id == item.Id);
            if (toUpdate == null)
            {
                return false;
            }
            var entry = contextProvider.Context.Entry(toUpdate);
            entry.OriginalValues.SetValues(toUpdate);
            entry.CurrentValues.SetValues(item);
            contextProvider.Context.SaveChanges();
            return true;
        }

        public string Metadata()
        {
            return contextProvider.Metadata();
        }
    }
}