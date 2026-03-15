using System;

public class ChoiceBonusActivator : IBonusActivator
{
    private IBonusChoiceMenu _choiceBonusMenu;
    private IBonusRandomizer _bonusRandomizer;
    private IBonusReservator _bonusReservator;
    private bool _isActive;

    public ChoiceBonusActivator(IBonusChoiceMenu choiceBonusMenu, IBonusRandomizer bonusRandomizer)
    {
        _choiceBonusMenu = choiceBonusMenu ?? throw new ArgumentNullException(nameof(choiceBonusMenu));
        _bonusRandomizer = bonusRandomizer ?? throw new ArgumentNullException(nameof(bonusRandomizer));
        _isActive = false;
    }

    public void Activate()
    {
        if (_isActive)
            throw new InvalidOperationException($"{nameof(ChoiceBonusActivator)} is already active.");

        _isActive = true;

        _choiceBonusMenu.BonusSelected += OnHandleBonus;
        _choiceBonusMenu.SetBonusRandomizer(_bonusRandomizer);
        _choiceBonusMenu.Enable();
    }

    public void SetBonusReservator(IBonusReservator bonusReservator)
    {
        _bonusReservator = bonusReservator ?? throw new ArgumentNullException(nameof(bonusReservator));
    }

    private void OnHandleBonus()
    {
        _choiceBonusMenu.BonusSelected -= OnHandleBonus;
        _isActive = false;

        var bonus = _choiceBonusMenu.SelectedBonus;

        if (_bonusReservator == null || _bonusReservator.TryAddBonusByName(bonus.BonusCard.Name) == false)
            bonus.Activate();
    }
}