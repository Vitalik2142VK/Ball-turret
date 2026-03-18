using System;
using System.Linq;

namespace CannonTurret.Utils
{
    public static class EnumHelper
    {
        public static int GetActiveValuesCount<T>() where T : Enum
        {
            return GetActiveValues<T>().Length;
        }

        public static T[] GetActiveValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Where(v => !IsObsolete(v))
                .ToArray();
        }

        private static bool IsObsolete<T>(T value)
        {
            var memInfo = typeof(T).GetMember(value.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(ObsoleteAttribute), false);
            return attributes.Length > 0;
        }
    }
}