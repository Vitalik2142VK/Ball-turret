using UnityEngine;

public class GlobalPosition : MonoBehaviour
{
    [SerializeField] private Vector3[] _worldPositions;
    [SerializeField, Min(1)] private int _positionsCount = 1;
    [SerializeField] private int _indexCurrentPoint;

    private void OnValidate()
    {
        if (_positionsCount != _worldPositions.Length)
            _worldPositions = new Vector3[_positionsCount];

        if (_indexCurrentPoint < 0 || _indexCurrentPoint >= _positionsCount)
            _indexCurrentPoint = 0;
    }

    [ContextMenu("Update World Transform")]
    private void UpdateWorldTransform()
    {
        _worldPositions[_indexCurrentPoint] = transform.position;
        _indexCurrentPoint++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}