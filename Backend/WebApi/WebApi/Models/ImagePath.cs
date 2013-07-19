using Newtonsoft.Json;

namespace WebApi.Models
{
    public class ImagePath
    {
        [JsonProperty]
        public string FullSizeImage { get; set; }

        [JsonProperty]
        public string PreviewSizeImage { get; set; }
    }
}