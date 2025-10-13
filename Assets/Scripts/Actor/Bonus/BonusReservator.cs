using System;
using System.Collections.Generic;
using System.Linq;

public class BonusReservator : IBonusReservator
{
    private Dictionary<string, IBonus> _reservedBonuses;

    public BonusReservator(IEnumerable<IBonus> reservedBonuses)
    {
        if (reservedBonuses == null)
            throw new ArgumentNullException(nameof(reservedBonuses));

        _reservedBonuses = new Dictionary<string, IBonus>();

        foreach (var bonus in reservedBonuses)
            _reservedBonuses.Add(bonus.BonusCard.Name, bonus);

        if (_reservedBonuses.Count == 0)
            throw new ArgumentException($"IEnumerable '{nameof(reservedBonuses)}' is empty");
    }

    public IEnumerable<IBonusCard> GetBonusCards()
    {
        return _reservedBonuses.Values.Select(b => b.BonusCard).ToArray();
    }

    public void ActivateBonus(string nameBonus)
    {
        if (string.IsNullOrEmpty(nameBonus))
            throw new ArgumentException(nameof(nameBonus));

        if (_reservedBonuses.ContainsKey(nameBonus) == false)
            throw new InvalidOperationException($"'{_reservedBonuses}' does not contain key '{nameof(nameBonus)}'");

        var bonus = _reservedBonuses[nameBonus];
        bonus.Activate();
    }
}
