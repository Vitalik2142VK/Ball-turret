public class WinState : IWinState
{
    public WinState()
    {
        IsWin = true;
    }

    public bool IsWin { get; set; }
}