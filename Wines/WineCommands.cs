using Microsoft.EntityFrameworkCore;
using MvcWine.Data;
using NyWine.RabbitMQ;

namespace NyWine.Wines
{
    public class WineCommands
    {
        private readonly MvcWineContext context;

        public WineCommands(MvcWineContext context)
        {
            this.context = context;
        }

        public async Task SaveWine(WineInfo wineInfo)
        {
            var wine = await context.GetOrInsertWine(wineInfo.ProductGuid);
            await SaveWineDescription(wineInfo, wine);
        }

        public async Task SaveWineDescription(WineInfo wineInfo, Wine wine)
        {
            // Get the last description
            var lastWineDescription = context.WineDescription
                .Where(wineDescription => wineDescription.WineId == wine.WineId)
                .OrderByDescending(description => description.ModifiedDate)
                .FirstOrDefault();

            // If the last description is different from the current description, add a new description
            if (lastWineDescription == null ||
                lastWineDescription.Name != wineInfo.Name ||
                lastWineDescription.Price != wineInfo.Price)
            {
                //Concurrency check if the last modified date has changed since the page was loaded
                var modifiedTicks = lastWineDescription?.ModifiedDate.Ticks ?? 0;
                if (modifiedTicks != wineInfo.LastModifiedTicks)
                {
                    throw new DbUpdateConcurrencyException("A new update has occurred since you loaded the page. Please refresh and try again.");
                }
                // Add a new description with the current date and time
                await context.AddAsync(new WineDescription
                {
                    ModifiedDate = DateTime.UtcNow,
                    Wine = wine,
                    Name = wineInfo.Name,
                    Description = wineInfo.Description,
                    Price = wineInfo.Price,
                    Origin = wineInfo.Origin,
                    AlcoholPercentage = wineInfo.AlcoholPercentage,
                    Year = wineInfo.Year,
                    Image = wineInfo.Image,
                    Size = wineInfo.Size,
                    //CategoryId = wineInfo.CategoryId
                });
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteWine(Guid productGuid)
        {
            var wine = await context.GetOrInsertWine(productGuid);
            await context.AddAsync(new WineRemoved
            {
                Wine = wine,
                RemovedDate = DateTime.UtcNow
            });
            await context.SaveChangesAsync();
        }
    }
}