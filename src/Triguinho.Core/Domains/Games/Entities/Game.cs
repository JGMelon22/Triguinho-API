using Triguinho.Core.Domains.Rounds.Entities;

namespace Triguinho.Core.Domains.Games.Entities;

public class Game
{
    protected Game()
    {
    }

    public Game(string name, string[] rules, bool isActive, ICollection<Round> rounds)
    {
        Name = name;
        Rules = rules;
        IsActive = isActive;
        CreateDate = DateTime.UtcNow;
        Rounds = rounds;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string[] Rules { get; set; } = [];
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public ICollection<Round> Rounds { get; set; } = [];
}