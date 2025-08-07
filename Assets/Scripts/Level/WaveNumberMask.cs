[System.Flags]
public enum WaveNumberMask
{
    None = 0,
    Boss = 1 << 0,
    WithBonuses1 = 1 << 1,
    WithBonuses2 = 1 << 2,
    WithBonuses3 = 1 << 3,
    WithoutBonuses = 1 << 4,
}