using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletRigidbodyPhysics : MonoBehaviour, IBulletPhysics
{
    [SerializeField] private Scriptable.BulletPhysicsAttributes _attributes;

    public event Action<GameObject> EnteredCollision;

    private Rigidbody _rigidbody;
    private Vector3 _currentDirection;
    private bool _isThereCollision;

    private void OnValidate()
    {
        if (_attributes == null)
            throw new NullReferenceException(nameof(_attributes));
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _rigidbody.isKinematic = false;
    }

    private void OnEnable()
    {
        _isThereCollision = false;
    }

    private void OnDisable()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision);

        EnteredCollision?.Invoke(collision.gameObject);
    }

    public void Activate()
    {
        Vector3 gravity = _attributes.Gravity * Vector3.back;
        _rigidbody.AddForce(gravity);

        _currentDirection = _rigidbody.velocity;
    }

    public void MoveToDirection(Vector3 direction)
    {
        _rigidbody.velocity = _attributes.Speed * direction;
    }

    private void HandleCollision(Collision collision)
    {
        if (_isThereCollision == false)
        {
            _isThereCollision = true;
            _rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
        }

        if (LayerMaskTool.IsInLayerMask(gameObject, _attributes.LayerMaskBounce))
        {
            Vector3 bounce = GetBounce(collision.contacts[0], _currentDirection);

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(bounce);
        }
    }

    private Vector3 GetBounce(ContactPoint contact, Vector3 direction)
    {
        Quaternion rotation = Quaternion.identity;
        Vector3 normal = contact.normal;
        Vector3 bounceDirection = Vector3.Reflect(direction, normal).normalized;
        float angle = Vector3.Angle(bounceDirection, normal);

        if (angle < _attributes.MinBounceAngle)
            rotation = CalculateRotation(normal, direction, _attributes.MinBounceAngle);
        else if (angle > _attributes.MaxBounceAngle)
            rotation = CalculateRotation(normal, direction, _attributes.MaxBounceAngle);

        bounceDirection = rotation * bounceDirection;

        return _attributes.BounceForce * bounceDirection;
    }

    private Quaternion CalculateRotation(Vector3 normal, Vector3 direction, float correctAngle)
    {
        Vector3 axis = Vector3.Cross(normal, direction).normalized;

        return Quaternion.AngleAxis(correctAngle, axis);
    }
}
