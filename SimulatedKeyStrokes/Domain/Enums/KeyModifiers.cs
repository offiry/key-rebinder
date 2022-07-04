using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    [Flags]
    public enum KeyModifiers
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8,
        NoRepeat = 0x4000,
        WithRepeat = 0x0000
    }
}
