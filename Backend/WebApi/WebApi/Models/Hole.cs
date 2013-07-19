using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Hole
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public HoleStatus Status { get; set; }
        public Location Location { get; set; }

        /// <summary>
        /// Gets or sets path to the fullsize image.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets path to the preview.
        /// </summary>
        public string PreviewPath { get; set; }
    }
}