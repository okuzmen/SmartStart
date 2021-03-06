﻿using System.Web.Http;
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

        [HttpPost]
        public JObject AddImage(JObject imageObject)
        {
            var image = imageObject.ToObject<ImageSource>();
            var result = repository.Add(image);
            return JObject.FromObject(result);
        }
    }
}