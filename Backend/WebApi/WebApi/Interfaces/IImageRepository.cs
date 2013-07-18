using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WebApi.Interfaces
{
    public interface IImageRepository
    {
        string[] Add(string encodedImage);
        void Update(Guid id, Image newImage);
        void Remove(Guid id);
    }
}
