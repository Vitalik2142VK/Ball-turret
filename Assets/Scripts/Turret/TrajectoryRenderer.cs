using Scriptable;
using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderer : MonoBehaviour, ITrajectoryRenderer
{
    [SerializeField] private BulletPhysics _bulletPhysics;
    [SerializeField] private TrajectoryBullet _trajectoryBullet;
    [SerializeField] private bool _isInstallFixUpdate = true;
    [SerializeField, Range(0.01f, 0.03f)] private float _timeStep = 0.02f;

    private LineRenderer _lineRenderer;
    private Transform _bulletPhysicsTransform;

    private void OnValidate()
    {
        if (_bulletPhysics == null)
            throw new NullReferenceException(nameof(_bulletPhysics));

        if (_trajectoryBullet == null)
            throw new NullReferenceException(nameof(_trajectoryBullet));

        if (_isInstallFixUpdate)
            _timeStep = Time.fixedDeltaTime;
    }

    private void Awake()
    {
        _bulletPhysicsTransform = _bulletPhysics.transform;

        _lineRenderer = GetComponent<LineRenderer>();

        Disable();
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

    public void Enable()
    {
        if (_lineRenderer.enabled == false)
            _lineRenderer.enabled = true;
    }

    public void Disable()
    {
        _lineRenderer.enabled = false;
    }
}
