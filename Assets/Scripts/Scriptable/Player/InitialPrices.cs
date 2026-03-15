using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Player/Initial prices", fileName = "InitialPrices", order = 51)]
    public class InitialPrices : ScriptableObject
    {
        [SerializeField, Min(100)] private int _damageImprovement;
        [SerializeField, Min(100)] private int _healthImprovement;

        public int DamageImprovement => _damageImprovement;
        public int HealthImprovement => _healthImprovement;
    }
}
