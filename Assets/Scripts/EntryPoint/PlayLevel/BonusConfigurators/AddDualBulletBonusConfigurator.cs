using System;
using UnityEngine;

namespace PlayLevel
{
    public class AddDualBulletBonusConfigurator : BonusConfigurator
    {
        private const int CountActivations = 2;

        [Header("Add bullet")]
        [SerializeField] private BulletFactory _bulletFactory;
        [SerializeField] private DefaultGun _gun;
        [SerializeField] private BulletType _bulletType;

        private void OnValidate()
        {
            if (_bulletFactory == null)
                throw new NullReferenceException(nameof(_bulletFactory));

            if (_gun == null)
                throw new NullReferenceException(nameof(_gun));
        }

        public override void Configure()
        {
            AddBulletBonusActivator addBulletBonusActivator = new AddBulletBonusActivator(_bulletFactory, _gun, _bulletType);
            MultipleBonusActivator multipleBonusActivator = new MultipleBonusActivator(addBulletBonusActivator, CountActivations);

            BonusPrefab.Initialize(multipleBonusActivator);
        }
    }
}
