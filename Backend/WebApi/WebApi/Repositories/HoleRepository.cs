using System;
using System.Collections.Generic;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class HoleRepository : IHoleRepository
    {
        private readonly List<Hole> products = new List<Hole>();
        private int nextId = 1;

        public HoleRepository()
        {
            Add(new Hole { Description = "Hole #1", Status = HoleStatus.New });
            Add(new Hole { Description = "Hole #2", Status = HoleStatus.New});
            Add(new Hole { Description = "Hole #3", Status = HoleStatus.Fixed});
        }

        public IEnumerable<Hole> GetAll()
        {
            return products;
        }

        public Hole Get(int id)
        {
            return products.Find(p => p.Id == id);
        }

        public Hole Add(Hole item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = nextId++;
            products.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            products.RemoveAll(p => p.Id == id);
        }

        public bool Update(Hole item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = products.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            products.RemoveAt(index);
            products.Add(item);
            return true;
        } 
    }
}