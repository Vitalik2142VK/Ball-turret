using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletsCollector : MonoBehaviour, IBulletRepository, IBonusStorage
{
    private List<IBullet> _bullets;
    private List<IBonus> _selectedBonuses;
    private int _countActiveBullets;
    private int _currentIndexBullet;

    public bool AreThereFreeBullets => _bullets.Count > _currentIndexBullet;
    public bool AreBulletsReturned => _countActiveBullets == 0;

    private void Awake()
    {
        _bullets = new List<IBullet>();
        _selectedBonuses = new List<IBonus>();
        _countActiveBullets = 0;
        _currentIndexBullet = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IBullet bullet))
            Put(bullet);
    }

    public bool TryGetBonuses(out IReadOnlyCollection<IBonus> bonuses)
    {
        bonuses = null;

        if (_selectedBonuses.Count == 0)
            return false;

        bonuses = _selectedBonuses;
        _selectedBonuses = new List<IBonus>();

        return true;
    }

    public void Add(IBullet bullet)
    {
        if (bullet == null)
            throw new ArgumentNullException(nameof(bullet));

        _bullets.Add(bullet);
    }

    public IBullet Get()
    {
        if (AreThereFreeBullets == false)
            throw new InvalidOperationException("The count of bullets is 0");

        IBullet bullet = _bullets[_currentIndexBullet];
        bullet.SetActive(true);

        _currentIndexBullet++;
        _countActiveBullets++;

        return bullet;
    }

    public void Put(IBullet bullet)
    {
        if (_bullets.Contains(bullet) == false)
            throw new InvalidOperationException("The bullet is not on the list");

        TakeBonuses(bullet);

        _countActiveBullets--;
        bullet.SetActive(false);

        if (AreBulletsReturned)
            _currentIndexBullet = 0;
    }

    private void TakeBonuses(IBullet bullet)
    {
        if (bullet.TryGetBonuses(out IReadOnlyCollection<IBonus> selectedBonuses))
            _selectedBonuses.AddRange(selectedBonuses);
    }
}
