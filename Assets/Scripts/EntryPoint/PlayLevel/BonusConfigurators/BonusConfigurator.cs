using UnityEngine;

namespace PlayLevel
{
    [RequireComponent(typeof(BonusCreator))]
    public abstract class BonusConfigurator : MonoBehaviour
    {
        public IBonus BonusPrefab { get; private set; }
        public BonusCreator Creator { get; private set; }


        private void Awake()
        {
            Creator = GetComponent<BonusCreator>();
            BonusPrefab = Creator.Create();
        }

        public abstract void Configure();
    }
}
