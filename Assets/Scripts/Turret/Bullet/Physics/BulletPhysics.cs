using System;
using UnityEngine;
using Scriptable;

[RequireComponent(typeof(Rigidbody))]
public class BulletPhysics : MonoBehaviour, IBulletPhysics
{
    [SerializeField] private AdvancedBulletPhysicsAttributes _attributes;
    [SerializeField] private TrajectoryBullet _trajectory;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField, Min(0)] private float _maxDirectionalError = 0.2f;

    public event Action<Collider> EnteredCollision;

    private Vector3 _velocity;
    private RaycastHit[] _hitsArray;
    private Transform _transform;
    private Rigidbody _rigidbody;
    private int _frame;
    private bool _isThereCollision;

    private void OnValidate()
    {
        if (_attributes == null)
            throw new NullReferenceException(nameof(_attributes));

        if (_trajectory == null)
            throw new NullReferenceException(nameof(_trajectory));
    }

    private void Awake()
    {
        _transform = transform;

        _hitsArray = new RaycastHit[1];

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void Activate()
    {
        float deltaTime = Time.fixedDeltaTime;

        if (_trajectory.IsEmpty || deltaTime != _trajectory.DeltaTime)
            Activate(deltaTime);

        if (_trajectory.HasFrame(_frame))
            MoveToPoint(deltaTime);
        else
            Activate(deltaTime);
    }

    public void MoveToDirection(Vector3 direction)
    {
        if (Vector3.Distance(direction, _trajectory.Direction) > _maxDirectionalError)
            _trajectory.Clear();

        _velocity = direction.normalized * _attributes.Speed;
        _isThereCollision = false;
        _frame = 0;
    }

    public void RecordPoint(float deltaTime)
    {
        if (_trajectory.IsEmpty || deltaTime != _trajectory.DeltaTime)
            CreateNewTrajectory(deltaTime);

        BulletTrajectoryPoint point = new BulletTrajectoryPoint(_frame++);

        UseGravity(deltaTime);

        if (TryGetCollision(out Collider collider))
            if (LayerMaskTool.IsInLayerMask(collider.gameObject, _attributes.LayerMaskBounce))
            {
                point.SetCollidedObject(collider);
                _trajectory.RecordCollision();
            }

        _rigidbody.MovePosition(_transform.position + _velocity);

        point.Position = _transform.position;
        point.Velocity = _velocity;
        _trajectory.AddPoint(point);
    }

    private void MoveToPoint(float deltaTime)
    {
        var point = _trajectory.GetPoint(_frame);

        if (point.IsThereCollision)
        {
            _isThereCollision = point.IsThereCollision;

            var collider = point.CollidedObject;

            if (collider == null || collider.enabled == false)
            {
                _trajectory.DeleteAfterFrame(_frame);

                Activate(deltaTime);

                return;
            }
            else
            {
                EnteredCollision?.Invoke(collider);
            }
        }

        _velocity = point.Velocity;
        _rigidbody.MovePosition(point.Position);
        _frame++;
    }

    private void CreateNewTrajectory(float deltaTime)
    {
        _trajectory.CreateNewTrajectory(deltaTime);
        _trajectory.AddPoint(new BulletTrajectoryPoint(_frame, _velocity, _transform.position));
        _frame++;
    }

    private void Activate(float deltaTime)
    {
        UseGravity(deltaTime);

        if (TryGetCollision(out Collider gameObject))
            EnteredCollision?.Invoke(gameObject);

        _rigidbody.MovePosition(_transform.position + _velocity);
    }

    private void UseGravity(float deltaTime)
    {
        if (_isThereCollision)
            _velocity.y = 0f;
        else
            _velocity += deltaTime * _attributes.Gravity * Vector3.down;

        _velocity += deltaTime * _attributes.Gravity * Vector3.back;
    }

    private bool TryGetCollision(out Collider gameObject)
    {
        gameObject = null;

        if (HasRaycastHit())
        {
            var hit = _hitsArray[0];
            gameObject = hit.collider;
            HandleCollision(hit);

            return true;
        }

        return false;
    }

    private bool HasRaycastHit()
    {
        float distance = _velocity.magnitude;
        Vector3 direction = _velocity.normalized;

        if (_isThereCollision == false)
            direction += Vector3.down;

        if (Physics.RaycastNonAlloc(_transform.position, direction, _hitsArray, distance, _collisionMask, QueryTriggerInteraction.Ignore) != 0)
        {
            _isThereCollision = true;

            return true;
        }

        return false;
    }

    private void HandleCollision(RaycastHit hit)
    {
        var gameObject = hit.transform.gameObject;

        if (LayerMaskTool.IsInLayerMask(gameObject, _attributes.LayerMaskBounce))
        {
            Vector3 direction = _velocity.normalized;
            Vector3 bounceDirection = GetBounce(hit.normal, direction);

            _velocity = bounceDirection;
        }
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
