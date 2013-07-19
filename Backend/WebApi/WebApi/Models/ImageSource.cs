using System.Drawing.Imaging;

namespace WebApi.Models
{
    public class ImageSource
    {
        public string DataUrl { get; set; }

        public string Get64BaseString()
        {
            return DataUrl.Substring(DataUrl.IndexOf(',') + 1);
        }

        public ImageFormat GetImageFormat()
        {
            return ImageFormat.Png;
        }
    }
}