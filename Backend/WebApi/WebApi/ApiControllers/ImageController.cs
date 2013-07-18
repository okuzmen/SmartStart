using System;
using System.Linq;
using System.Web.Http;
using Breeze.WebApi;
using Newtonsoft.Json.Linq;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.ApiControllers
{
    [BreezeController]
    public class ImageController : ApiController
    {
        private readonly IImageRepository repository;

        public ImageController(IImageRepository repository)
        {
            this.repository = repository;
        }

        //[HttpGet]
        //public string GetImagePath(Guid id)
        //{
        //    return repository.GetAll();
        //}

        [HttpGet]
        public string[] AddImage(string encodedImage)
        {
            return repository.Add(encodedImage);
        }
    }
}