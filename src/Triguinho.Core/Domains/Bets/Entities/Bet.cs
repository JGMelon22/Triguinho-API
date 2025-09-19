using Triguinho.Core.Domains.Bets.Enums;
using Triguinho.Core.Domains.Rounds.Entities;

namespace Triguinho.Core.Domains.Bets.Entities;

public class Bet
{
    protected Bet()
    {
    }

    public Bet(int roundId, decimal depositValue, decimal multiplier)
    {
        RoundId = roundId;
        DepositValue = depositValue;
        Multiplier = multiplier;
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

    public int RoundId { get; set; }
    public Round? Round { get; set; }
}