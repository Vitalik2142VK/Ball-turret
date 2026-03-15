using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Health attributes", fileName = "HealthAttributes", order = 51)]
    public class HealthAttributes : ScriptableObject, IHealthAttributes
    {
        [SerializeField, Min(10f)] private float _maxHealth; 
        
        public float MaxHealth => _maxHealth;
    }
}
