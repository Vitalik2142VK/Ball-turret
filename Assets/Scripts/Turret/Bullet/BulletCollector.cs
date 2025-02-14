using System.Collections.Generic;
using UnityEngine;

public class BulletCollector : MonoBehaviour, IBonusStorage
{
    private List<IBonus> _selectedBonuses;

    private void Awake()
    {
        _selectedBonuses = new List<IBonus>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IBullet bullet))
        {
            if (bullet.TryGetBonuses(out List<IBonus> selectedBonuses))
                _selectedBonuses.AddRange(selectedBonuses);

            bullet.EndFlight();
        }
    }

    public bool TryGetBonuses(out List<IBonus> bonuses)
    {
        bonuses = null;

        if (_selectedBonuses.Count == 0)
            return false;

        bonuses = _selectedBonuses;
        _selectedBonuses = new List<IBonus>();

        return true;
    }
}
