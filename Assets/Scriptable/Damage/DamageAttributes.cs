using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Damage attributes", fileName = "DamageAttributes", order = 51)]
    public class DamageAttributes : ScriptableObject, IDamageAttributes
    {
        [SerializeField, Min(0f)] private float _damage;

        public float Damage => _damage;
    }
}
