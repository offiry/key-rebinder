using Application.Contracts;
using Application.Queries.Contracts;
using Domain.Contracts;
using Domain.Enums;
using Domain.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application.Handlers.Start
{
    public class StartGameBindingsQuery : IRequest
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string WindowGameName { get; set; }
    }

    public class StartGameBindingsHandler : IRequestHandler<StartGameBindingsQuery>
    {
        private readonly IMediator _mediator;
        private readonly IGameCacheService _gameCacheService;
        private readonly IGameKeysRepository _gameKeysRepository;
        private readonly IGameKeysQueryRepository _gameKeysQueryRepository;

        public StartGameBindingsHandler(IMediator mediator, IGameCacheService gameCacheService, IGameKeysRepository gameKeysRepository,
            IGameKeysQueryRepository gameKeysQueryRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _gameCacheService = gameCacheService ?? throw new ArgumentNullException(nameof(gameCacheService));
            _gameKeysRepository = gameKeysRepository ?? throw new ArgumentNullException(nameof(gameKeysRepository));
            _gameKeysQueryRepository = gameKeysQueryRepository ?? throw new ArgumentNullException(nameof(gameKeysQueryRepository));
        }

        public async Task<Unit> Handle(StartGameBindingsQuery request, CancellationToken cancellationToken)
        {
            var keyAggRoots = _gameKeysQueryRepository
                .GetGameKeysEntities(request.GameId);

            foreach (var key in keyAggRoots)
            {
                _gameCacheService
                    .Add(await _mediator.Send(new KeyBindingQuery
                    {
                        WindowGameName = request.WindowGameName,
                        Key = EnumUtil.ParseEnum<Keys>(key.KeyEntity.Key),
                        KeyModifier = EnumUtil.ParseEnum<KeyModifiers>(key.KeyModifierEntity.Key),
                        TargetKey = EnumUtil.ParseEnum<Keys>(key.TargetKeyEntity.Key)
                    }, cancellationToken));
            }

            return Unit.Value;
        }
    }
}
