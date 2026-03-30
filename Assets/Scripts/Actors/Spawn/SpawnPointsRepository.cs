using System;
using UnityEngine;

namespace CannonTurret.Actors.Spawn
{
    public class SpawnPointsRepository : MonoBehaviour, ISpawnPointsRepository
    {
        private const int ColumnCount = 3;
        private const int LinesCount = 3;
        private const float DistanceBetweenPoints = 2.5f;
        private const float IntervalBetweenPoints = 2f;

        [SerializeField] private Transform _startPoint;

        private Vector3[,] _spawnPoints;
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

            return _spawnPoints[lineNum, columnNum];
        }

        private Vector3[,] CreatePoints()
        {
            Vector3[,] spawnPoints = new Vector3[LinesCount, ColumnCount];
            float positionX = _positionStartPoint.x;
            float positionZ = _positionStartPoint.z;

            for (int i = 0; i < LinesCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    spawnPoints[i, j] = new Vector3(positionX, _positionStartPoint.y, positionZ);

                    positionX += IntervalBetweenPoints;
                }

                positionZ -= DistanceBetweenPoints;
                positionX = _positionStartPoint.x;
            }

            return spawnPoints;
        }
    }
}