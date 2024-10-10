using Microsoft.EntityFrameworkCore;
using Resenhando2.Api.Data.Mappings;
using Resenhando2.Core.Entities.Review;

namespace Resenhando2.Api.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ReviewMapping());
    }
}