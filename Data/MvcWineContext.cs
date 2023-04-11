using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NyWine.Models;

namespace MvcWine.Data
{
    public class MvcWineContext : DbContext
    {
        public MvcWineContext (DbContextOptions<MvcWineContext> options)
            : base(options)
        {
        }

        public DbSet<NyWine.Models.Wine> Wine { get; set; } = default!;
    }
}
