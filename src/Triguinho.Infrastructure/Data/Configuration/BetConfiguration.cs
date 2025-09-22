using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Triguinho.Core.Domains.Bets.Entities;

namespace Triguinho.Infrastructure.Data.Configuration;

public class BetConfiguration : IEntityTypeConfiguration<Bet>
{
    public void Configure(EntityTypeBuilder<Bet> builder)
    {
        builder.ToTable("Bets");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

        builder.Property(b => b.DepositValue)
            .HasColumnName("DepositValue")
            .HasColumnType("DECIMAL")
            .HasPrecision(10, 2)
            .IsRequired();


        builder.Property(b => b.Multiplier)
            .HasColumnName("Multiplier")
            .HasColumnType("DECIMAL")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(b => b.Status)
            .HasConversion<string>()
            .HasColumnName("Status")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(b => b.PrizeValue)
            .HasColumnName("PrizeValue")
            .HasColumnType("DECIMAL")
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.Property(b => b.BetDate)
            .HasColumnName("BetDate")
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.Property(b => b.RoundId)
            .HasColumnName("RoundId")
            .HasColumnType("INT")
            .IsRequired();

        // Owned Types
        builder.OwnsOne(b => b.GuessMade, guess =>
        {
            guess.Property(p => p.ChosenValue)
                .HasColumnName("GuessValueChosen")
                .IsRequired();

            guess.Property(p => p.BetType)
                .HasColumnName("GuessTypeBet")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.HasOne(b => b.Round)
            .WithMany(r => r.Bets)
            .HasForeignKey(b => b.RoundId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(b => b.Id)
            .HasDatabaseName("IDX_Bets_BetId");

        builder.HasIndex(b => b.RoundId)
            .HasDatabaseName("IDX_Bets_RoundId");

        builder.HasIndex(b => b.Status)
            .HasDatabaseName("IDX_Bets_Status"); ;

        builder.HasIndex(b => b.BetDate)
            .HasDatabaseName("IDX_Bets_BetDate"); ;
    }
}