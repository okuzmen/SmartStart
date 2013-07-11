using System.Linq;
using System.Web.Http;
using Breeze.WebApi;
using Newtonsoft.Json.Linq;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.ApiControllers
{
    [BreezeController]
    public class HoleController : ApiController
    {
        private readonly IHoleRepository repository;

        public HoleController(IHoleRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public string Metadata()
        {
            return repository.Metadata();
        }

        [HttpGet]
        public IQueryable<Hole> GetHoles()
        {
            return repository.GetAll();
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return repository.SaveChanges(saveBundle);
        }
    }
}