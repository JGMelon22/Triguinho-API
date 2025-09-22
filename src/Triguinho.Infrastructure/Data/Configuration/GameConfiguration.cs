using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Triguinho.Core.Domains.Games.Entities;

namespace Triguinho.Infrastructure.Data.Configuration;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

        builder.Property(g => g.Name)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(g => g.Rules)
            .HasConversion(
                rules => string.Join(",", rules),
                value => value.Split(",",
                StringSplitOptions.RemoveEmptyEntries)
            )
            .HasColumnName("Rules")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder.HasMany(g => g.Rounds)
            .WithOne(r => r.Game)
            .HasForeignKey(r => r.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(g => g.Id)
            .HasDatabaseName("IDX_Game_Id");

        builder.HasIndex(g => g.Name)
            .HasDatabaseName("IDX_Game_Name");

        builder.HasIndex(g => g.IsActive)
            .HasDatabaseName("IDX_Game_IsActive");
    }
}