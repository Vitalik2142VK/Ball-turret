namespace CannonTurret.UI.Animations
{
    public interface IButtonAnimator
    {
        public bool IsPressed { get; }

        public void Press();

        public void PressOut();
    }
}