using CannonTurret.Actors.Bonuses;
using System;

namespace CannonTurret.UI.PlayerScene
{
    public interface IBonusChoiceMenu : IWindow
    {
        public event Action BonusSelected;

        public IBonus SelectedBonus { get; }

        public void SetBonusRandomizer(IBonusRandomizer randomizer);
    }
}