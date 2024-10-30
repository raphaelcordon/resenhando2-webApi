using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resenhando2.Core.Entities;

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
        
        builder.Property(x => x.CoverImage)
            .IsRequired()
            .HasColumnName("CoverImage")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);
        
        builder.Property(x => x.ReviewTitle)
            .IsRequired()
            .HasColumnName("ReviewTitle")
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);
        
        builder.Property(x => x.ReviewBody)
            .IsRequired()
            .HasColumnName("ReviewBody")
            .HasColumnType("NVARCHAR(MAX)");

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnName("UserId")
            .HasColumnType("UNIQUEIDENTIFIER");
        
        builder.Property(x => x.YouTubeId)
            .IsRequired(false)
            .HasColumnName("YouTubeId")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(30);
        
        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIMEOFFSET");
    }
}