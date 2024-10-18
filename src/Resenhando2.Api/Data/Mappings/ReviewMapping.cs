using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resenhando2.Core.Entities.Review;

namespace Resenhando2.Api.Data.Mappings;

public class ReviewMapping : IEntityTypeConfiguration<Review>
{

    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Review");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ReviewType)
            .IsRequired()
            .HasColumnName("ReviewType")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(30);

        builder.Property(x => x.SpotifyId)
            .IsRequired()
            .HasColumnName("SpotifyId")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(30);

        builder.OwnsOne(x => x.ReviewText,
            reviewText =>
            {
                reviewText.Property(x => x.ReviewTitle)
                    .IsRequired()
                    .HasColumnName("ReviewText")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(50);

                reviewText.Property(x => x.ReviewBody)
                    .IsRequired()
                    .HasColumnName("ReviewBody")
                    .HasColumnType("NVARCHAR(MAX)");
            });

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnName("UserId")
            .HasColumnType("uniqueidentifier");
    }
}