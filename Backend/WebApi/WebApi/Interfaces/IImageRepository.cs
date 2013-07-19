using System;
using WebApi.Models;
using Image = System.Drawing.Image;

namespace WebApi.Interfaces
{
    public interface IImageRepository
    {
        ImagePath Add(string encodedImage);
        void Update(Guid id, Image newImage);
        void Remove(Guid id);
    }
}
