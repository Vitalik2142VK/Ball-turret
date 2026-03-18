using UnityEngine;

namespace CannonTurret.UI.Animations
{
    public interface IAnimatorUI
    {
        public void Show();

        public void Hide();

        public YieldInstruction GetYieldAnimation();
    }
}