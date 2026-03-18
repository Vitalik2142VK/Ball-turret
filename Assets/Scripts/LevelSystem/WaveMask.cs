using System;

namespace CannonTurret.LevelSystem
{
    [Flags]
    public enum WaveMask
    {
        [Obsolete("Empty mask is not allowed", true)]
        None = 0,

        Boss = 1 << 0,
        WithBonuses1 = 1 << 1,
        WithBonuses2 = 1 << 2,
        WithBonuses3 = 1 << 3,
        WithoutBonuses = 1 << 4,
    }
}