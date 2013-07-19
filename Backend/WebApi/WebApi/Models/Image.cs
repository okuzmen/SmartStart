namespace WebApi.Models
{
    public class Image
    {
        public string ImageResource { get; set; }

        public string Get64BaseString()
        {
            return ImageResource.Substring(ImageResource.IndexOf(',') + 1);
        }
    }
}