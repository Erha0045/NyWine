using System.ComponentModel.DataAnnotations;

namespace NyWine.Wines
{
    // Skal fjerne denne
    public class Wine
    {       
        public int WineId { get; set; }
       [Required]
        public Guid ProductGuid { get; set; }
        public ICollection<WineDescription> Descriptions { get; set; } = new List<WineDescription>();
        public ICollection<WineRemoved> Removed { get; set; } = new List<WineRemoved>();
    }

}