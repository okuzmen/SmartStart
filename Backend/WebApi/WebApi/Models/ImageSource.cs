using System.Drawing.Imaging;
using Newtonsoft.Json;

namespace WebApi.Models
{
    public class ImageSource
    {
        [JsonProperty("dataUrl")]
        public string DataUrl { get; set; }

        public string Get64BaseString()
        {
            return DataUrl.Substring(DataUrl.IndexOf(',') + 1);
        }
    }
}