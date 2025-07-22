using System;
using UnityEngine;
using Scriptable;

[RequireComponent(typeof(Rigidbody))]
public class BulletRigidbodyPhysics : MonoBehaviour, IBulletPhysics
{
    [SerializeField] private BulletPhysicsAttributes _attributes;
    [SerializeField] private TrajectoryBullet _trajectory;

    public event Action<GameObject> EnteredCollision;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private Vector3 _currentDirection;
    private int _frame;
    private bool _isThereCollision;
    private bool _isRecordingGoing;

    private void OnValidate()
    {
        if (_attributes == null)
            throw new NullReferenceException(nameof(_attributes));
    }

    private void Awake()
    {
        _transform = transform;

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _rigidbody.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var gameObject = collision.gameObject;

        HandleCollision(gameObject, collision.contacts[0].normal);

        if (_isRecordingGoing == false)
            EnteredCollision?.Invoke(gameObject);
    }

    public void Activate()
    {
        _isRecordingGoing = false;

        float deltaTime = Time.fixedDeltaTime;

        if (_trajectory.IsEmpty || deltaTime != _trajectory.DeltaTime)
            UsePhysicsRigidbody();

        if (_trajectory.HasFrame(_frame))
            MoveToPoint();
        else
            UsePhysicsRigidbody();
    }

    public void MoveToDirection(Vector3 direction)
    {
        _isThereCollision = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _rigidbody.velocity = _attributes.Speed * direction;
    }

    public void RecordPoint(float deltaTime)
    {
        _rigidbody.isKinematic = false;

        if (_trajectory.IsEmpty || deltaTime != _trajectory.DeltaTime)
            CreateNewTrajectory(deltaTime);

        BulletTrajectoryPoint point = new BulletTrajectoryPoint(_frame++);

        UsePhysicsRigidbody();

        if (_rigidbody.SweepTest(_currentDirection.normalized, out RaycastHit hit, _currentDirection.magnitude * deltaTime, QueryTriggerInteraction.Ignore))
        {
            var gameObject = hit.collider.gameObject;

            HandleCollision(gameObject, hit.normal);

            if (LayerMaskTool.IsInLayerMask(gameObject, _attributes.LayerMaskBounce))
            {
                point.SetCollidedGameObject(gameObject);
                _trajectory.RecordCollision();
            }
        }

        point.Position = _transform.position;
        point.Velocity = _rigidbody.velocity;
        _trajectory.AddPoint(point);
    }

    private void MoveToPoint()
    {
        var point = _trajectory.GetPoint(_frame);

        if (point.IsThereCollision)
        {
            _isThereCollision = point.IsThereCollision;

            var gameObject = point.CollidedGameObject;

            if (gameObject == null || gameObject.activeSelf == false)
            {
                _trajectory.DeleteAfterFrame(_frame);

                UsePhysicsRigidbody();

                return;
            }
            else
            {
                EnteredCollision?.Invoke(gameObject);
            }
        }

        _rigidbody.velocity = point.Velocity;
        _rigidbody.MovePosition(point.Position);
        _frame++;
    }

    private void CreateNewTrajectory(float deltaTime)
    {
        _trajectory.CreateNewTrajectory(deltaTime);
        _trajectory.AddPoint(new BulletTrajectoryPoint(_frame, _rigidbody.velocity, _transform.position));
        _frame++;
    }

    private void UsePhysicsRigidbody()
    {
        Vector3 gravity = _attributes.Gravity * Vector3.back;
        _rigidbody.AddForce(gravity);
        _currentDirection = _rigidbody.velocity;
    }

    private void HandleCollision(GameObject gameObject, Vector3 normal)
    {
        if (_isThereCollision == false)
        {
            _isThereCollision = true;
            _rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
        }

        if (LayerMaskTool.IsInLayerMask(gameObject, _attributes.LayerMaskBounce))
        {
            Vector3 bounce = GetBounce(normal, _currentDirection);

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(bounce);
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
