﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletPhysics : MonoBehaviour, IBulletPhysics
{
    private IBulletPhysicsAttributes _attributes;
    private Rigidbody _rigidbody;
    private Vector3 _currentDirection;
    private bool _isThereCollision = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnDisable()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void Initialize(IBulletPhysicsAttributes attributes)
    {
        if (attributes == null)
            throw new System.ArgumentNullException(nameof(attributes));

        _attributes ??= attributes;
    }

    public void UseGravity()
    {
        Vector3 gravity = _attributes.Gravity * Vector3.back;
        _rigidbody.AddForce(gravity);

        _currentDirection = _rigidbody.velocity;
    }

    public void MoveToDirection(Vector3 direction)
    {
        _rigidbody.velocity = _attributes.Speed * direction;
    }

    public void HandleCollision(Collision collision)
    {
        if (_isThereCollision == false)
        {
            _isThereCollision = true;
            _rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
        }

        if (IsInLayerMask(collision.gameObject))
        {
            Vector3 bounce = GetBounce(collision.contacts[0], _currentDirection);

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(bounce);
        }
    }

    private bool IsInLayerMask(GameObject gameObject)
    {
        return (1 << gameObject.layer & _attributes.LayerMaskBounce) != 0;
    }

    private Vector3 GetBounce(ContactPoint contact, Vector3 direction)
    {
        Vector3 normal = contact.normal;
        Vector3 bounceDirection = Vector3.Reflect(direction, normal).normalized;
        float angle = Vector3.Angle(bounceDirection, normal);

        if (angle < _attributes.MinBounceAngle)
        {
            float correctAngle = _attributes.MinBounceAngle;
            Vector3 axis = Vector3.Cross(normal, direction).normalized;
            Quaternion rotation = Quaternion.AngleAxis(correctAngle, axis);
            bounceDirection = rotation * bounceDirection;
        }

        return _attributes.BounceForce * bounceDirection;
    }
}
