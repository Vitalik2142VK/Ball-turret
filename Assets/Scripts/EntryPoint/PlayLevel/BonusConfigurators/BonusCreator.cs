using System;
using UnityEngine;

namespace PlayLevel
{
    public class BonusCreator : MonoBehaviour
    {
        [SerializeField] private Scriptable.BonusCard _bonusCard;

        private void OnValidate()
        {
            if (_bonusCard == null)
                throw new NullReferenceException(nameof(_bonusCard));
        }

        public IBonus Create()
        {
            return new Bonus(_bonusCard);
        }
    }
}
