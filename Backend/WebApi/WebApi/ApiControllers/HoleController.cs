using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.ApiControllers
{
    public class HoleController : ApiController
    {
        private static readonly IHoleRepository repository = new HoleRepository();

        [HttpGet]
        public IEnumerable<Hole> Get()
        {
            return repository.GetAll();
        }

        [HttpGet]
        public Hole Get(int id)
        {
            return repository.Get(id);
        }

        [HttpPost]
        public HttpResponseMessage Post(Hole item)
        {
            repository.Add(item);
            var response = Request.CreateResponse(HttpStatusCode.Created, item);

            //RouteTable.Routes.MapPageRoute()
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [HttpPut]
        public void Put(int id, Hole item)
        {
            item.Id = id;
            if (!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}