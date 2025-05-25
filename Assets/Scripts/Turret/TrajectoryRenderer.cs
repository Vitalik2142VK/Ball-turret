using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private BulletPhysics _bulletPhysics;
    [SerializeField, Range(0.01f, 0.03f)] private float _timeStep = 0.02f;
    [SerializeField, Min(30)] private int _countCalculations = 100;

    private LineRenderer _lineRenderer;
    private Transform _bulletPhysicsTransform;

    private void OnValidate()
    {
        if (_bulletPhysics == null)
            throw new NullReferenceException(nameof(_bulletPhysics));
    }

    private void Awake()
    {
        _bulletPhysicsTransform = _bulletPhysics.transform;

        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void ShowTrajectory(Vector3 origin, Vector3 direction)
    {
        Vector3[] points = new Vector3[_countCalculations];

        Physics.simulationMode = SimulationMode.Script;

        _bulletPhysicsTransform.position = origin;
        _bulletPhysics.MoveToDirection(direction);

        for (int i = 0; i < _countCalculations; i++)
        {
            Physics.Simulate(_timeStep);

            _bulletPhysics.Activate();
            points[i] = _bulletPhysicsTransform.position;
        }

        _lineRenderer.positionCount = points.Length;
        _lineRenderer.SetPositions(points);

        Physics.simulationMode = SimulationMode.FixedUpdate;
    }
}
