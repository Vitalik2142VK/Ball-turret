using System;
using UnityEngine;

namespace PlayLevel
{
    public class ImprovedHealthConfigurator : MonoBehaviour
    {
        [SerializeField] private Scriptable.EnemyAttributes[] _enemyAttributes;

        private void OnValidate()
        {
            if (_enemyAttributes == null || _enemyAttributes.Length == 0)
                throw new InvalidOperationException(nameof(_enemyAttributes));
        }

        public void Configure(float healthCoefficient)
        {
            foreach (var healthAttributes in _enemyAttributes)
            {
                healthAttributes.Improve(healthCoefficient);
            }
        }
    }
}
