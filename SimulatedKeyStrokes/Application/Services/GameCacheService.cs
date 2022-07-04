using Application.Contracts;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class GameCacheService : IGameCacheService
    {
        private List<IHotKeyService> _hotKeyServices = new List<IHotKeyService>();

        public void Add(IHotKeyService hotKeyService)
        {
            _hotKeyServices.Add(hotKeyService);
        }

        public void DisposeAll()
        {
            foreach (var hotKeyService in _hotKeyServices)
            {
                hotKeyService.Dispose();
            }

            _hotKeyServices.Clear();
        }
    }
}
