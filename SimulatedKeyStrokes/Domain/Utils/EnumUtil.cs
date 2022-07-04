using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Utils
{
    public static class EnumUtil
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
