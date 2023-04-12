using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NyWine.Wines;

namespace MvcWine.Data
{
    public class MvcWineContext : DbContext
    {
        public MvcWineContext (DbContextOptions<MvcWineContext> options)
            : base(options)
        {
        }

        public DbSet<Wine> Wine { get; set; }
        public DbSet<WineDescription> WineDescription { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wine>()
                .HasAlternateKey(w => new { w.ProductGuid });
            
            modelBuilder.Entity<WineDescription>()
                .HasAlternateKey(wineDescription => new { wineDescription.WineId, wineDescription.ModifiedDate });
        }

         public async Task<Wine> GetOrInsertWine(Guid productGuid)
        {
            var wine = Wine
                .Include(wine => wine.Descriptions)
                .Where(wine => wine.ProductGuid == productGuid)
                .SingleOrDefault();
            if (wine == null)
            {
                wine = new Wine
                {
                    ProductGuid = productGuid
                };
                await AddAsync(wine);
            }

            return wine;
        }
    }
}
