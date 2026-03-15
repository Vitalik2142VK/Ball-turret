using System;
using System.Collections.Generic;
using System.Linq;

public class BonusReservator : IBonusReservator
{
    private const int MaxCountBonuses = 3;

    private Dictionary<string, IReservatedBonus> _reservedBonuses;

    public BonusReservator(IEnumerable<IBonus> reservedBonuses)
    {
        if (reservedBonuses == null)
            throw new ArgumentNullException(nameof(reservedBonuses));

        _reservedBonuses = CreateDictionaryPrefabs(reservedBonuses);

        IsBonusActivated = false;
    }

    public bool IsBonusActivated { get; private set; }

    public IEnumerable<IReservatedBonus> Bonuses => _reservedBonuses.Values.ToArray();
    public bool HasBonuses => IsCanActivateBonuses();

    public void ActivateBonus(string nameBonus)
    {
        if (IsBonusActivated)
            return;

        if (string.IsNullOrEmpty(nameBonus))
            throw new ArgumentException(nameof(nameBonus));

        if (_reservedBonuses.ContainsKey(nameBonus) == false)
            throw new InvalidOperationException($"'{_reservedBonuses}' does not contain key '{nameof(nameBonus)}'");

        var bonus = _reservedBonuses[nameBonus];

        if (bonus.IsCanActivate)
        {
            bonus.Activate();

            IsBonusActivated = true;
        }
    }

    public bool TryAddBonusByName(string nameBonus)
    {
        if (string.IsNullOrEmpty(nameBonus))
            throw new ArgumentException(nameof(nameBonus));

        if (_reservedBonuses.ContainsKey(nameBonus) == false)
            return false;

        var bonus = _reservedBonuses[nameBonus];

        return bonus.TryAddBonus(nameBonus);
    }

    public void Update()
    {
        IsBonusActivated = false;
    }

    private Dictionary<string, IReservatedBonus> CreateDictionaryPrefabs(IEnumerable<IBonus> reservedBonuses)
    {
        Dictionary<string, IReservatedBonus> result = new Dictionary<string, IReservatedBonus>();

        ReservatedBonus reservatedBonus;

        foreach (var bonus in reservedBonuses)
        {
            reservatedBonus = new ReservatedBonus(bonus, MaxCountBonuses);
            result.Add(bonus.BonusCard.Name, reservatedBonus);
        }

        if (result.Count == 0)
            throw new ArgumentException($"IEnumerable '{nameof(reservedBonuses)}' is empty");

        return result;
    }

    private bool IsCanActivateBonuses() 
    {
        foreach(var bonus in _reservedBonuses.Values)
            if (bonus.IsCanActivate)
                return true;

        return false;
    }
}
