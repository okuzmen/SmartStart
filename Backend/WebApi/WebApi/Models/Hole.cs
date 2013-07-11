using System.ComponentModel.DataAnnotations;
using System.Data.Spatial;

namespace WebApi.Models
{
    public class Hole
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public HoleStatus Status { get; set; }
        public DbGeography Location { get; set; }

        /// <summary>
        /// Gets or sets path to the image.
        /// </summary>
        public string ImagePath { get; set; }
    }
}