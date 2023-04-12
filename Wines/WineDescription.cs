using System.ComponentModel.DataAnnotations;

namespace NyWine.Wines
{
    public class WineDescription
    {
        public int WineDescriptionId { get; set; }

        public Wine Wine { get; set; }
        public int WineId { get; set; }
        public DateTime ModifiedDate { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(100)]
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public float AlcoholPercentage { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Image { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string Size { get; set; }
    }
}