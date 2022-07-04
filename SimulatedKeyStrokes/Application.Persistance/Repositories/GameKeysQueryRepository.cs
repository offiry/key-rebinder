using Application.Queries.Contracts;
using Domain.Model.AggRoot;
using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Persistance.Repositories
{
    public class GameKeysQueryRepository : IGameKeysQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GameKeysQueryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public List<KeyAggRoot> GetGameKeysEntities(int gameId)
        {
            var keysQuery = from gk in _applicationDbContext.GameKeysEntities
                            join g in _applicationDbContext.GameEntities on gk.WindowGameNameId_FK equals gameId
                            join km in _applicationDbContext.KeyModifierEntities on gk.KeyModifierId_FK equals km.Id
                            join k1 in _applicationDbContext.KeysEntities on gk.KeyId_FK equals k1.Id
                            join k2 in _applicationDbContext.KeysEntities on gk.TargetKey_FK equals k2.Id
                            select new KeyAggRoot
                            {
                                GameEntity = new GameEntity { Id = gameId, WindowGameName = g.WindowGameName },
                                KeyModifierEntity = new KeyModifierEntity { Id = k1.Id, Key = km.Key, KeyModifier = km.KeyModifier },
                                KeyEntity = new KeyEntity { Id = k1.Id, Key = k1.Key },
                                TargetKeyEntity = new KeyEntity { Id = k2.Id, Key = k2.Key }
                            };
            return keysQuery.ToList();
        }
    }
}
