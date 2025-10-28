using System;

public interface IBonusChoiceMenu : IWindow
{
    public event Action BonusSelected;

    public IBonus SelectedBonus { get; }

    public void SetBonusRandomizer(IBonusRandomizer randomizer);
}