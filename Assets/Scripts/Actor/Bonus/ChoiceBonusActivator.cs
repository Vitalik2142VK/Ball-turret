using UnityEngine;

public class ChoiceBonusActivator : IBonusActivator
{
    private IMenu _choiceBonusMenu;

    public ChoiceBonusActivator(IMenu choiceBonusMenu)
    {
        _choiceBonusMenu = choiceBonusMenu ?? throw new System.ArgumentNullException(nameof(choiceBonusMenu));
    }

    public void Activate()
    {
        _choiceBonusMenu.Enable();
    }

    public IBonusActivator Clone()
    {
        return new ChoiceBonusActivator(_choiceBonusMenu);
    }
}