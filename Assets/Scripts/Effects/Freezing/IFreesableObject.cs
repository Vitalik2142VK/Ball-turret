namespace CannonTurret.Effects.Freezing
{
    public interface IFreesableObject
    {
        public bool HasIceShell { get; }

        public void Freeze(IIceShell iceShell);
    }
}