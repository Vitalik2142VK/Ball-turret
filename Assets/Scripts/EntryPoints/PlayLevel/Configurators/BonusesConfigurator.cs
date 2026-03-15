using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayLevel
{
    public class BonusesConfigurator : MonoBehaviour
    {
        [SerializeField] private Scriptable.BonusCard[] _reservatedBonusCards;
        [SerializeField] private BonusConfigurator[] _bonusConfigurators;
        [SerializeField] private ChoiceBonusActivatorCreator[] _choiceBonusActivatorCreators;
        [SerializeField] private BigBangBonusActivatorCreator _bigBangBonusActivatorCreator;
        [SerializeField] private ReservedBonusesWindow _reservavedBonusesWindow;
        [SerializeField] private ViewableBonusFactory _bonusFactory;

        public IBonusReservator BonusReservator { get; private set; }

        private void OnValidate()
        {
            if (_reservatedBonusCards == null || _reservatedBonusCards.Length == 0)
                throw new InvalidOperationException(nameof(_reservatedBonusCards));

            foreach (var reservatedBonusCard in _reservatedBonusCards)
                if (reservatedBonusCard == null)
                    throw new NullReferenceException($"{_reservatedBonusCards} contains null objects");

            if (_bonusConfigurators == null || _bonusConfigurators.Length == 0)
                throw new InvalidOperationException(nameof(_bonusConfigurators));

            foreach (var bonusConfigurator in _bonusConfigurators)
                if (bonusConfigurator == null)
                    throw new NullReferenceException($"{_bonusConfigurators} contains null objects");

            if (_choiceBonusActivatorCreators == null || _choiceBonusActivatorCreators.Length == 0)
                throw new InvalidOperationException(nameof(_reservatedBonusCards));

            foreach (var activatorCreator in _choiceBonusActivatorCreators)
                if (activatorCreator == null)
                    throw new NullReferenceException($"{_choiceBonusActivatorCreators} contains null objects");

            if (_bigBangBonusActivatorCreator == null)
                throw new NullReferenceException(nameof(_bigBangBonusActivatorCreator));

            if (_reservavedBonusesWindow == null)
                throw new NullReferenceException(nameof(_reservavedBonusesWindow));

            if (_bonusFactory == null)
                throw new NullReferenceException(nameof(_bonusFactory));
        }

        public void Configure(IEnemyCounter enemyCounter)
        {
            if (enemyCounter == null)
                throw new NullReferenceException(nameof(enemyCounter));

            _bigBangBonusActivatorCreator.Initialize(enemyCounter);

            var bonusCreators = _bonusConfigurators.Select(bc => bc.Creator).ToArray();

            ConfigureBonuses();
            ConfigureReservatedBonuses(bonusCreators);
            InitializeActivatorCreators();
            InitializeBonusFactory(bonusCreators);
        }

        private void ConfigureBonuses()
        {
            foreach (var bonusConfigurator in _bonusConfigurators)
                bonusConfigurator.Configure();
        }

        private void ConfigureReservatedBonuses(IEnumerable<IBonusCreator> bonusCreators)
        {
            BonusFactory bonusFactory = new BonusFactory(bonusCreators);
            List<IBonus> bonuses = new List<IBonus>();

            foreach (var bonusCard in _reservatedBonusCards)
                bonuses.Add(bonusFactory.Create(bonusCard.Name));

            BonusReservator = new BonusReservator(bonuses);
            _reservavedBonusesWindow.Initialize(BonusReservator);
        }

        private void InitializeActivatorCreators()
        {
            foreach (var activatorCreators in _choiceBonusActivatorCreators)
                activatorCreators.SetBonusReservator(BonusReservator);
        }

        private void InitializeBonusFactory(IEnumerable<IBonusCreator> bonusCreators)
        {
            
            List<IViewableBonusCreator> collisionBonusCreators = new List<IViewableBonusCreator>();

            foreach (var creator in bonusCreators)
                if (creator is IViewableBonusCreator viewableBonusCreator)
                    collisionBonusCreators.Add(viewableBonusCreator);

            _bonusFactory.Initialize(collisionBonusCreators);
        }
    }
}
