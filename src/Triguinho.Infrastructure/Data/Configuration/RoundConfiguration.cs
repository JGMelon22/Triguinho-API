using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Triguinho.Core.Domains.Rounds.Entities;

namespace Triguinho.Infrastructure.Data.Configuration;

public class RoundConfiguration : IEntityTypeConfiguration<Round>
{
    public void Configure(EntityTypeBuilder<Round> builder)
    {
        builder.ToTable("Rounds");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

        builder.Property(r => r.SequenceNumber)
            .HasColumnName("SequenceNumber")
            .HasColumnType("INT")
            .IsRequired();

        builder.Property(r => r.StartDate)
            .HasColumnName("StartDate")
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.Property(r => r.EndDate)
            .HasColumnName("EndDate")
            .HasColumnType("DATETIME")
            .IsRequired(false);

        builder.Property(r => r.Status)
            .HasConversion<string>()
            .HasColumnName("Status")
            .HasColumnType("VARCHAR")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(r => r.GameId)
            .HasColumnName("GameId")
            .HasColumnType("INT")
            .IsRequired();

        // Owned Types
        builder.OwnsOne(r => r.GeneratedResult, result =>
        {
            result.Property(res => res.DrawnValue)
                .HasColumnName("ResultDrawnAmount")
                .IsRequired();

            result.Property(res => res.Description)
                .HasColumnName("ResultDescription")
                .HasMaxLength(200)
                .IsRequired();

            result.Property(res => res.GenerationMoment)
                .HasColumnName("ResultGenerationMoment")
                .IsRequired();
        });

        builder.HasOne(r => r.Game)
            .WithMany(g => g.Rounds)
            .HasForeignKey(r => r.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Bets)
            .WithOne(b => b.Round)
            .HasForeignKey(b => b.RoundId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => r.GameId)
            .HasDatabaseName("IDX_Round_GameId");

        builder.HasIndex(r => r.Status)
            .HasDatabaseName("IDX_Round_Status");

        builder.HasIndex(r => new { r.GameId, r.SequenceNumber })
            .IsUnique();
    }
}