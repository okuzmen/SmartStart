using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Hole
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public HoleStatus Status { get; set; }
        
        /// <summary>
        /// Gets or sets coordinates of the hole.
        /// Actually, I don't sure about datatype. Depends on several factors: DB and map api.
        /// Should be changed to correct datatype. 
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets path to the image.
        /// </summary>
        public string Image { get; set; }
    }
}