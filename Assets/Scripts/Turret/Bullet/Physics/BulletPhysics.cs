using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletPhysics : MonoBehaviour, IBulletPhysics
{
    [SerializeField] private Scriptable.AdvancedBulletPhysicsAttributes _attributes;
    [SerializeField] private LayerMask _collisionMask;

    public event Action<GameObject> EnteredCollision;

    private Vector3 _velocity;
    private Transform _transform;
    private Rigidbody _rigidbody;
    private bool _isThereCollision;

    private void OnValidate()
    {
        if (_attributes == null)
            throw new NullReferenceException(nameof(_attributes));
    }

    private void Awake()
    {
        _transform = transform;

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    private void OnEnable()
    {
        _isThereCollision = false;
    }

    public void Activate()
    {
        float deltaTime = Time.fixedDeltaTime;

        UseGravity(deltaTime);
        CheckCollision();
    }

    public void MoveToDirection(Vector3 direction)
    {
        _velocity = direction.normalized * _attributes.Speed;
    }

    private void UseGravity(float deltaTime)
    {
        if (_isThereCollision)
            _velocity.y = 0f;
        else
            _velocity += deltaTime * _attributes.Gravity * Vector3.down;

        _velocity += deltaTime * _attributes.Gravity * Vector3.back;
        _rigidbody.MovePosition(_transform.position + _velocity);
    }

    private void CheckCollision()
    {
        float distance = _velocity.magnitude;
        Vector3 direction = _velocity.normalized;

        if (_isThereCollision == false)
            direction += Vector3.down;

        if (Physics.Raycast(_transform.position, direction, out RaycastHit hit, distance, _collisionMask, QueryTriggerInteraction.Ignore))
        {
            HandleCollision(hit);
            EnteredCollision?.Invoke(hit.transform.gameObject);
        }
    }

    private void HandleCollision(RaycastHit hit)
    {
        var gameObject = hit.transform.gameObject;

        FixHeight(gameObject);

        if (LayerMaskTool.IsInLayerMask(gameObject, _attributes.LayerMaskBounce))
        {
            Vector3 direction = _velocity.normalized;
            Vector3 bounceDirection = GetBounce(hit.normal, direction);

            _velocity = bounceDirection;

            Debug.Log($"Raycast hit = {hit.transform.gameObject} || direction = {direction} || bounceDirection = {bounceDirection} || _velocity = {_velocity}");
        }
    }

    private void FixHeight(GameObject gameObject)
    {
        if (_isThereCollision)
            return;

        if (LayerMaskTool.IsInLayerMask(gameObject, _collisionMask))
            _isThereCollision = true;
    }

    private Vector3 GetBounce(Vector3 normal, Vector3 direction)
    {
        normal = new Vector3(normal.x, 0f, normal.z);
        direction = new Vector3(direction.x, 0f, direction.z);
        Quaternion rotation = Quaternion.identity;
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