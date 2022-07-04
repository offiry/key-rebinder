using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts
{
    public interface IGameCacheService
    {
        void Add(IHotKeyService hotKeyService);
        void DisposeAll();
    }
}
