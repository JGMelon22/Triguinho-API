namespace Triguinho.Core.Domains.Bets.ValueObjects;

public class Guess
{
    protected Guess()
    {
    }

    public Guess(int chosenValue, string betType)
    {
        ChosenValue = chosenValue;
        BetType = betType;
    }

    public int ChosenValue { get; set; }
    public string BetType { get; set; } = string.Empty;
}