using Triguinho.Core.Domains.Bets.Entities;
using Triguinho.Core.Domains.Games.Entities;
using Triguinho.Core.Domains.Rounds.Enums;
using Triguinho.Core.Domains.Rounds.ValueObjects;

namespace Triguinho.Core.Domains.Rounds.Entities;

public class Round
{
    protected Round()
    {
    }

    public Round(int gameId, int sequenceNumber)
    {
        GameId = gameId;
        SequenceNumber = sequenceNumber;
        StartDate = DateTime.UtcNow;
        Status = RoundStatus.Open;
        Bets = new List<Bet>();
    }

    public int Id { get; set; }
    public int SequenceNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public RoundStatus Status { get; set; }

    // A result only exists when a round ends
    public Result? GeneratedResult { get; set; }

    public int GameId { get; set; }
    public Game? Game { get; set; }

    public ICollection<Bet> Bets { get; set; } = [];
}