

namespace NyWine.Models
{
    public class Wine
    {
        // [Column("wine_id")]
        public int Id { get; set; }
        // [Column("product_uuid")]
        public Guid ProductGuid { get; set; }
        // [Column("wine_name")]
        public string Name { get; set; }
        // [Column("wine_description")]
        public string Description { get; set; }
        // [Column("price")]
        public decimal Price { get; set; }
        // [Column("wine_origin")]
        public string Origin { get; set; }
        // [Column("alcohol_percentage")]
        public float AlcoholPercentage { get; set; }
        // [Column("production_year")]
        public int Year { get; set; }
        // [Column("image")]
        public string Image { get; set; }
        // [Column("bottle_size")]
        public string Size { get; set; }
        public int CategoryId { get; set; }
        public DateTime ModifiedDate { get; set; }
     
    }

}

