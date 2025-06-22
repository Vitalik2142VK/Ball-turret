using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayLevel
{
    public class BonusesPrefabConfigurator : MonoBehaviour
    {
        [SerializeField] private BonusConfigurator[] _bonusConfigurators;
        [SerializeField] private BonusFactory _bonusFactory;
        [SerializeField] private BonusChoiceMenu _bonusChoiceMenu;
        [SerializeField] private Sound _takenBonusSound;

        private ITurret _turret;

        private void OnValidate()
        {
            if (_bonusConfigurators == null)
                throw new NullReferenceException(nameof(_bonusConfigurators));

            if (_bonusConfigurators.Length == 0)
                throw new InvalidOperationException(nameof(_bonusConfigurators));

            if (_bonusFactory == null)
                throw new NullReferenceException(nameof(_bonusFactory));

            if (_bonusChoiceMenu == null)
                throw new NullReferenceException(nameof(_bonusChoiceMenu));

            if (_takenBonusSound == null)
                throw new NullReferenceException(nameof(_takenBonusSound));
        }

        public void Configure(ITurret turret)
        {
            _turret = turret ?? throw new ArgumentNullException(nameof(turret));

            foreach (var bonusConfigurator in _bonusConfigurators)
            {
                ConfigureAdditionally(bonusConfigurator);

                bonusConfigurator.Configure();
            }

            InitializeBonusFactory();
            InitializeChoiceBonusMenu();
        }

        private void ConfigureAdditionally(BonusConfigurator configurator)
        {
            if (configurator is FullHealthTurretBonusConfigurator fullHealthTurretBonusConfigurator)
                fullHealthTurretBonusConfigurator.SetHealthTurret(_turret);
        }

        private void InitializeBonusFactory()
        {
            var bonusPrefabs = _bonusConfigurators.Select(bc => bc.BonusPrefab).ToArray();
            List<CollisionBonus> collisionBonuses = new List<CollisionBonus>();

            foreach (var bonusPrefab in bonusPrefabs)
            {
                if (bonusPrefab.TryGetComponent(out CollisionBonus collisionBonus))
                    collisionBonuses.Add(collisionBonus);
            }

            _bonusFactory.Initialize(collisionBonuses, _takenBonusSound);
        }

        private void InitializeChoiceBonusMenu()
        {
            var bonusPrefabs = _bonusConfigurators.Where(bc => bc.IsItRandom).Select(bc => bc.BonusPrefab).ToArray();
            BonusRandomizer bonusRandomizer = new BonusRandomizer(bonusPrefabs);
            _bonusChoiceMenu.Initialize(bonusRandomizer);
        }
    }
}
