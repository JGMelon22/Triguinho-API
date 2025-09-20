using Triguinho.Core.Domains.Rounds.Dtos.Requests;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Domains.Rounds.Entities;

namespace Triguinho.Core.Domains.Rounds.Mappings;

public static class MappingExtensions
{
    public static Round ToDomain(this CreateRoundRequest request, int sequenceNumber)
        => new Round(request.GameId, sequenceNumber);

    public static RoundResponse ToResponse(this Round round)
        => new RoundResponse
        {
            Id = round.Id,
            SequenceNumber = round.SequenceNumber,
            StartDate = round.StartDate,
            EndDate = round.EndDate,
            Status = round.Status,
            GameId = round.GameId,
            GameName = round.Game?.Name!,
            // Owned Type Result Mapping
            DrawnValue = round.GeneratedResult?.DrawnValue,
            ResultDescription = round.GeneratedResult?.Description,
            GenerationMoment = round.GeneratedResult?.GenerationMoment
        };

    public static IEnumerable<RoundResponse> ToResponse(this IEnumerable<Round> rounds)
    {
        return rounds.Select(round =>
            new RoundResponse
            {
                Id = round.Id,
                SequenceNumber = round.SequenceNumber,
                StartDate = round.StartDate,
                EndDate = round.EndDate,
                Status = round.Status,
                GameId = round.GameId,
                GameName = round.Game?.Name!,
                // Owned Type Result Mapping
                DrawnValue = round.GeneratedResult?.DrawnValue,
                ResultDescription = round.GeneratedResult?.Description,
                GenerationMoment = round.GeneratedResult?.GenerationMoment
            }
        );
    }
}