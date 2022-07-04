using Domain.Enums;
using Domain.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Domain.Contracts
{
    public interface IHotKeyService : IDisposable
    {
        IHotKeyService RegisterHotKeyService(Keys key, KeyModifiers modifiers, GameKeyDto gameKeyDto);
    }
}
