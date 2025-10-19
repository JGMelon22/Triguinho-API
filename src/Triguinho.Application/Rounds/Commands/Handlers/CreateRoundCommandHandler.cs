using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;
using Triguinho.Core.Domains.Rounds.Mappings;
using Microsoft.Extensions.Logging;

namespace Triguinho.Application.Rounds.Commands.Handlers
{
    public class CreateRoundCommandHandler : IRequestHandler<CreateRoundCommand, Result<RoundResponse>>
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<CreateRoundCommandHandler> _logger;

        public CreateRoundCommandHandler(IRoundRepository roundRepository, IGameRepository gameRepository, ILogger<CreateRoundCommandHandler> logger)
        {
            _roundRepository = roundRepository;
            _gameRepository = gameRepository;
            _logger = logger;
        }

        public async Task<Result<RoundResponse>> Handle(CreateRoundCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var game = await _gameRepository.ReadAsync(request.Request.GameId);

                if (game == null)
                    return Result<RoundResponse>.Failure(Error.GameNotFound);

                if (!game.IsActive)
                {
                    _logger.LogWarning("Game with Id {Id} is inactive", request.Request.GameId);
                    return Result<RoundResponse>.Failure(Error.GameInactive);
                }

                var sequenceNumber = await _roundRepository.GetNextSequenceNumberAsync(request.Request.GameId);
                var round = request.Request.ToDomain(sequenceNumber);

                var createdRound = await _roundRepository.CreateAsync(round);

                if (createdRound == null)
                    return Result<RoundResponse>.Failure(Error.RoundCreationFailed);

                var response = createdRound.ToResponse();

                return Result<RoundResponse>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to create round. GameId: {GameId}, SequenceNumber: {SequenceNumber}",
                    request.Request.GameId,
                    request.SequenceNumber);
                throw;
            }
        }
    }
}