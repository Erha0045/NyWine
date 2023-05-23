using Microsoft.EntityFrameworkCore;
using MvcWine.Data;
namespace NyWine.Wines
{
    public class WineQueries
    {
         private readonly MvcWineContext context;

        public WineQueries(MvcWineContext context)
        {
            this.context = context;
        }

        public async Task<List<WineInfo>> ListWines()
        {
            var result = await context.Wine
                .Where(wine => !wine.Removed.Any())
                .Select(wine => new
                {
                    wine.ProductGuid,
                    Description = wine.Descriptions
                        .OrderByDescending(d => d.ModifiedDate)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return result
                .Select(row => WineInfo.FromEntities(row.ProductGuid, row.Description))
                .ToList();
        }

        public async Task<WineInfo> GetWine(Guid productGuid)
        {
            var result = await context.Wine
                .Where(wine => wine.ProductGuid == productGuid &&
                    !wine.Removed.Any())
                .Select(wine => new
                {
                    wine.ProductGuid,
                    Description = wine.Descriptions
                        .OrderByDescending(d => d.ModifiedDate)
                        .FirstOrDefault()
                })
                .SingleOrDefaultAsync();

            return result == null ? null : WineInfo.FromEntities(result.ProductGuid, result.Description);
        }

        private object MapWine(Guid productGuid, WineDescription wineDescription)
        {
            return new WineInfo
            {
                ProductGuid = productGuid,
                Name = wineDescription?.Name,
                Description = wineDescription?.Description,
                Price = (decimal)(wineDescription?.Price),
                Origin = wineDescription?.Origin,
                AlcoholPercentage = (float)(wineDescription?.AlcoholPercentage),
                Year = (int)(wineDescription?.Year),
                Image = wineDescription?.Image,
                Size = wineDescription?.Size,
                LastModifiedTicks = wineDescription?.ModifiedDate.Ticks ?? 0,
                //CategoryId = (int)wineDescription?.CategoryId
            };
        }
    }
}