using Newtonsoft.Json;

namespace WebApi.Models
{
    public class ImagePath
    {
        [JsonProperty("fullSizeImage")]
        public string FullSizeImage { get; set; }

        [JsonProperty("previewSizeImage")]
        public string PreviewSizeImage { get; set; }
    }
}