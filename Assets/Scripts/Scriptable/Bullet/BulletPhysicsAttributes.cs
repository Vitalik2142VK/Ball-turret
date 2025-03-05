using UnityEngine;

[CreateAssetMenu(menuName = "Attributes/Bullet Physics Attributes", fileName = "BulletPhysicsAttributes", order = 51)]
public class BulletPhysicsAttributes : ScriptableObject, IBulletPhysicsAttributes
{
    [SerializeField, Min(0.1f)] private float _speed;
    [SerializeField, Min(0.1f)] private float _gravity;
    [SerializeField, Min(0.1f)] private float _bounceForce;
    [SerializeField] private LayerMask _layerMaskBounce;

    public float Speed => _speed;
    public float Gravity => _gravity;
    public float BounceForce => _bounceForce;
    public LayerMask LayerMaskBounce => _layerMaskBounce;
}

