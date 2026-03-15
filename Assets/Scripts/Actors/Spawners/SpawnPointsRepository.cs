using System;
using UnityEngine;

public class SpawnPointsRepository : MonoBehaviour, ISpawnPointsRepository
{
    private const int ColumnCount = 3;
    private const int LinesCount = 3;
    private const float DistanceBetweenPoints = 2.5f;
    private const float IntervalBetweenPoints = 2f;

    [SerializeField] private Transform _startPoint;

    private SpawnPoint[,] _spawnPoints;
    private Vector3 _positionStartPoint;

    private void OnValidate()
    {
        if (_startPoint == null)
            throw new NullReferenceException(nameof(_spawnPoints));
    }

    private void Awake()
    {
        _positionStartPoint = _startPoint.position;
        _spawnPoints = CreatePoints();
    }

    public Vector3 GetPositionSpawnPoint(int columnNum, int lineNum)
    {
        if (columnNum < 0 || columnNum >= ColumnCount)
            throw new ArgumentOutOfRangeException($"The {nameof(columnNum)} must be from 0 to {ColumnCount - 1}");

        if (lineNum < 0 || lineNum >= LinesCount)
            throw new ArgumentOutOfRangeException($"The {nameof(lineNum)} must be from 0 to {LinesCount - 1}");

        SpawnPoint spawnPoint = _spawnPoints[lineNum, columnNum];

        return spawnPoint.GetPosition();
    }

    public void FreeAllSpawnPoints()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
            for (int j = 0; j < _spawnPoints.GetLength(0); j++)
                _spawnPoints[i,j].FreePoint();
    }

    private SpawnPoint[,] CreatePoints()
    {
        SpawnPoint[,] spawnPoints = new SpawnPoint[LinesCount, ColumnCount];
        Vector3 positionNewPoint = _positionStartPoint;

        for (int i = 0; i < LinesCount; i++, positionNewPoint = new Vector3(positionNewPoint.x, positionNewPoint.y, positionNewPoint.z - DistanceBetweenPoints))
        {
            for (int j = 0; j < ColumnCount; j++, positionNewPoint = new Vector3(positionNewPoint.x + IntervalBetweenPoints, positionNewPoint.y, positionNewPoint.z))
                spawnPoints[i, j] = new SpawnPoint(positionNewPoint);

            positionNewPoint = new Vector3(_positionStartPoint.x, positionNewPoint.y, positionNewPoint.z);
        }

        return spawnPoints;
    }
}
