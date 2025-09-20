using Triguinho.Core.Domains.Bets.Enums;
using Triguinho.Core.Domains.Bets.ValueObjects;
using Triguinho.Core.Domains.Rounds.Entities;

namespace Triguinho.Core.Domains.Bets.Entities;

public class Bet
{
    protected Bet()
    {
    }

    public Bet(int roundId, decimal depositValue, decimal multiplier, Guess guessMade)
    {
        RoundId = roundId;
        DepositValue = depositValue;
        Multiplier = multiplier;
        GuessMade = guessMade;
        Status = BetStatus.Pending;
        BetDate = DateTime.Now;
        PrizeValue = 0;
    }

    public int Id { get; set; }
    public decimal DepositValue { get; set; }
    public decimal Multiplier { get; set; }
    public BetStatus Status { get; set; }
    public decimal PrizeValue { get; set; }
    public DateTime BetDate { get; set; }

    // Owned Type - A Guess is obligatory when create a bet

    public Guess GuessMade { get; set; } = null!;

    public int RoundId { get; set; }
    public Round? Round { get; set; }
}