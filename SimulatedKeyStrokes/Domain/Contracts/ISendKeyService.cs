using Domain.Model.Dto;
using System;

namespace Domain.Contracts
{
    public interface ISendKeyService : IDisposable
    {
        void SendKey(GameKeyDto gameKeyDto);
    }
}
