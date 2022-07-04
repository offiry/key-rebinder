using Domain.Model.AggRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Contracts
{
    public interface IGameKeysQueryRepository
    {
        List<KeyAggRoot> GetGameKeysEntities(int gameId);
    }
}
