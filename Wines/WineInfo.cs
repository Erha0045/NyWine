namespace NyWine.Wines
{
    public class WineInfo
    {
        public Guid ProductGuid { get; set; }
        public string Name { get; set; }
       
        public string Description { get; set; }
       
        public decimal Price { get; set; }
    
        public string Origin { get; set; }
   
        public float AlcoholPercentage { get; set; }
    
        public int Year { get; set; }
       
        public string Image { get; set; }
        
        public string Size { get; set; }
        public long LastModifiedTicks { get; set; }

        // public int CategoryId { get; set; }
        // public Category Category { get; set; }

        public static WineInfo FromEntities(Guid productGuid, WineDescription description)
        {
            return new WineInfo
            {
                ProductGuid = productGuid,
                Name = description?.Name,
                Description = description?.Description,
                Price = (decimal)(description?.Price),
                Origin = description?.Origin,
                AlcoholPercentage = (float)(description?.AlcoholPercentage),
                Year = (int)(description?.Year),
                Image = description?.Image,
                Size = description?.Size,
                LastModifiedTicks = description?.ModifiedDate.Ticks ?? 0,
                //CategoryId = (int)description?.CategoryId
            };
            
        }
    }
}
