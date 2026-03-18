using CannonTurret.Actors.Bonuses.Activators;
using CannonTurret.Scriptable.Bonus;
using System;
using UnityEngine;

namespace CannonTurret.Actors.Bonuses
{
    public class BonusCreator : MonoBehaviour, IBonusCreator
    {
        [SerializeField] private BonusCard _bonusCard;

        private IBonusActivator _bonusActivator;

        public string Name => _bonusCard.Name;

        private void OnValidate()
        {
            if (_bonusCard == null)
                throw new NullReferenceException(nameof(_bonusCard));
        }

        public void Initialize(IBonusActivator bonusActivator)
        {
            _bonusActivator = bonusActivator ?? throw new ArgumentNullException(nameof(bonusActivator));
        }

        public IBonus Create()
        {
            return new Bonus(_bonusCard, _bonusActivator);
        }
    }
}