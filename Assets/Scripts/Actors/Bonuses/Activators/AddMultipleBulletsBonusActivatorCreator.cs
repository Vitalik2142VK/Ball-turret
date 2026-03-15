using System;
using UnityEngine;

public class AddMultipleBulletsBonusActivatorCreator : MonoBehaviour, IBonusActivatorCreator
{
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField] private Gun _gun;
    [SerializeField, Min(2)] private int _countBullets;

    private void OnValidate()
    {
        if (_bulletFactory == null)
            throw new NullReferenceException(nameof(_bulletFactory));

        if (_gun == null)
            throw new NullReferenceException(nameof(_gun));
    }

    public IBonusActivator Create()
    {
        AddBulletBonusActivator addBulletBonusActivator = new AddBulletBonusActivator(_bulletFactory, _gun, BulletType.Default);

        return new MultipleBonusActivator(addBulletBonusActivator, _countBullets);
    }
}
