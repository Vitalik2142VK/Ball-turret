using UnityEngine;

namespace PlayLevel
{
    public abstract class BonusConfigurator: MonoBehaviour
    {
        [SerializeField, SerializeIterface(typeof(IBonusCreator))] private GameObject _bonusCreator;

        public IBonus BonusPrefab { get; private set; }
        public IBonusCreator Creator { get; private set; }

        private void OnValidate()
        {
            if (_bonusCreator == null)
                throw new System.NullReferenceException(nameof(_bonusCreator));
        }

        private void Awake()
        {
            Creator = _bonusCreator.GetComponent<BonusCreator>();
            BonusPrefab = Creator.Create();
        }

        public abstract void Configure();
    }
}
