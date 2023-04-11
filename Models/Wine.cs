

using System.ComponentModel.DataAnnotations;

namespace NyWine.Models
{
    public class Wine
    {
        
        public int WineId { get; set; }
       [Required]
        public Guid ProductGuid { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public string Origin { get; set; }
        
        public float AlcoholPercentage { get; set; }
        
        public int Year { get; set; }
        // [Column("image")]
        public string Image { get; set; }
        // [Column("bottle_size")]
        public string Size { get; set; }
   
     
    }

}

