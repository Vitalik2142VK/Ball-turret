using Scriptable;
using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderer : MonoBehaviour, ITrajectoryRenderer
{
    [SerializeField, SerializeIterface(typeof(IBulletPhysics))] private GameObject _bulletImitator;
    [SerializeField] private TrajectoryBullet _trajectoryBullet;
    [SerializeField] private bool _isInstallFixUpdate = true;
    [SerializeField, Range(0.01f, 0.03f)] private float _timeStep = 0.02f;

    private IBulletPhysics _bulletPhysics;
    private LineRenderer _lineRenderer;
    private Transform _bulletPhysicsTransform;

    private void OnValidate()
    {
        if (_bulletImitator == null)
            throw new NullReferenceException(nameof(_bulletImitator));

        if (_trajectoryBullet == null)
            throw new NullReferenceException(nameof(_trajectoryBullet));

        if (_isInstallFixUpdate)
            _timeStep = Time.fixedDeltaTime;
    }

    private void Awake()
    {
        _bulletPhysics = _bulletImitator.GetComponent<IBulletPhysics>();
        _bulletPhysicsTransform = _bulletImitator.transform;
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.alignment = LineAlignment.TransformZ;
        _lineRenderer.numCapVertices = 0;
        _lineRenderer.numCornerVertices = 0;
        _lineRenderer.widthMultiplier = 0.2f;

        transform.forward = Camera.main.transform.forward;

        Disable();
    }

    public void Enable()
    {
        if (_lineRenderer.enabled == false)
            _lineRenderer.enabled = true;
    }

    public void Disable()
    {
        _lineRenderer.enabled = false;
    }

    public void ShowTrajectory(Vector3 origin, Vector3 direction)
    {
        if (direction == _trajectoryBullet.Direction)
            return;

        _bulletPhysicsTransform.position = origin;
        _bulletPhysics.MoveToDirection(direction);
        _trajectoryBullet.Direction = direction;
        _trajectoryBullet.Clear();
        _trajectoryBullet.CreateNewTrajectory(_timeStep);

        Physics.simulationMode = SimulationMode.Script;

        for (int i = 0; i < _trajectoryBullet.MaxCountPoints && _trajectoryBullet.IsFinished == false; i++)
        {
            Physics.Simulate(_timeStep);

            _bulletPhysics.RecordPoint(_timeStep);
        }

        Physics.simulationMode = SimulationMode.FixedUpdate;

        Vector3[] points = _trajectoryBullet.GetPointsPosition();
        _lineRenderer.positionCount = points.Length;
        _lineRenderer.SetPositions(points);
    }

    public void Clear()
    {
        _lineRenderer.positionCount = 0;
        _trajectoryBullet.Direction = Vector3.zero;
        _trajectoryBullet.Clear();
    }
}
