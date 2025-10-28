using System;
using UnityEngine;

namespace PlayLevel
{
    public class BonusConfigurator : MonoBehaviour
    {
        [SerializeField, SerializeIterface(typeof(IBonusCreator))] private GameObject _bonusCreator;
        [SerializeField, SerializeIterface(typeof(IBonusActivatorCreator))] private GameObject _bonusActivatorCreator;

        private IBonusActivatorCreator _activatorCreator;

        public IBonusCreator Creator { get; private set; }

        private void OnValidate()
        {
            if (_bonusCreator == null)
                throw new NullReferenceException(nameof(_bonusCreator));

            if (_bonusCreator.TryGetComponent(out IBonusActivatorCreator _))
                _bonusActivatorCreator = _bonusCreator;

            if (_bonusActivatorCreator == null)
                throw new NullReferenceException(nameof(_bonusActivatorCreator));

            if (_bonusActivatorCreator.TryGetComponent(out IBonusCreator _))
                _bonusCreator = _bonusActivatorCreator;
        }

        private void Awake()
        {
            _activatorCreator = _bonusActivatorCreator.GetComponent<IBonusActivatorCreator>();
            Creator = _bonusCreator.GetComponent<IBonusCreator>();
        }

        public void Configure()
        {
            IBonusActivator activator = _activatorCreator.Create();
            Creator.Initialize(activator);
        }
    }
}
