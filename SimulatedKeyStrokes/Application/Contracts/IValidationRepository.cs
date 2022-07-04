using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts
{
    public interface IValidationRepository
    {
        bool IsGameExists(int gameId);
    }
}
