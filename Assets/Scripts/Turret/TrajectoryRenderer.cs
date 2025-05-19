using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private Bullet _prefabBullet;
    [SerializeField, Range(0.01f, 0.03f)] private float _timeStep;
    [SerializeField, Min(30)] private int _count—alculations;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void ShowTrajectory(Vector3 origin, Vector3 direction)
    {
        Bullet bullet = Instantiate(_prefabBullet, origin, Quaternion.identity);
        bullet.Move(origin, direction);

        Physics.simulationMode = SimulationMode.Script;

        Vector3[] points = new Vector3[_count—alculations];

        for (int i = 0; i < points.Length; i++)
        {
            Physics.Simulate(_timeStep);

            points[i] = bullet.transform.position;
        }

        _lineRenderer.positionCount = points.Length;
        _lineRenderer.SetPositions(points);

        Destroy(bullet.gameObject);

        Physics.simulationMode = SimulationMode.FixedUpdate;
    }
}
