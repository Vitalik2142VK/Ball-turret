using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Attributes/Advanced bullet physics attributes", fileName = "AdvancedBulletPhysicsAttributes", order = 51)]
    public class AdvancedBulletPhysicsAttributes : ScriptableObject, IBulletPhysicsAttributes
    {
        [SerializeField] private BulletPhysicsAttributes _attributes;
        [SerializeField, Min(0.001f)] private float _coefficientDeltaTime;
        [SerializeField, Min(0.01f)] private float _coefficientBounceForce;

        public float Speed => _attributes.Speed * _coefficientDeltaTime;
        public float Gravity => _attributes.Gravity * _coefficientDeltaTime;
        public float BounceForce => _attributes.BounceForce * _coefficientDeltaTime * _coefficientBounceForce;
        public float MinBounceAngle => _attributes.MinBounceAngle;
        public float MaxBounceAngle => _attributes.MaxBounceAngle;
        public LayerMask LayerMaskBounce => _attributes.LayerMaskBounce;

        private void OnValidate()
        {
            if (_attributes == null)
                throw new System.NullReferenceException(nameof(_attributes));
        }
    }
}
