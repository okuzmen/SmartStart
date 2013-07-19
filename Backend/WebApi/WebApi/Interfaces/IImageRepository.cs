using System;
using System.Drawing;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IImageRepository
    {
        ImagePath Add(ImageSource image);
        void Update(Guid id, Image newImage);
        void Remove(Guid id);
    }
}
