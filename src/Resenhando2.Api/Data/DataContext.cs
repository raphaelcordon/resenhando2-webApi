using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Resenhando2.Api.Data.Mappings;
using Resenhando2.Core.Entities.Identity;
using Resenhando2.Core.Entities.Review;

namespace Resenhando2.Api.Data;

public class DataContext(DbContextOptions<DataContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ReviewMapping());
    }
}