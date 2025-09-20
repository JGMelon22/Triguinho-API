namespace Triguinho.Core.Domains.Rounds.ValueObjects;

public class Result
{
    protected Result() { }



    public Result(int drawnValue, string description, DateTime generationMoment)
    {
        DrawnValue = drawnValue;
        Description = description;
        GenerationMoment = generationMoment;
    }

    public int DrawnValue { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public DateTime GenerationMoment { get; private set; }
}