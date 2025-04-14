using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Gun Attributes", fileName = "GunAttributes", order = 51)]
    public class GunAttributes : ScriptableObject, IGunAttributes
    {
        [SerializeField, Range(0.1f, 2.0f)] private float _timeBetweenShots;
        [SerializeField, Min(1)] private int _initialCountBulltes;

        public float TimeBetweenShots => _timeBetweenShots;
        public int InitialCountBulltes => _initialCountBulltes;
    }
}
