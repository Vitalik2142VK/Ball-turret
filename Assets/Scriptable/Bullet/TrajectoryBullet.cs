using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Trajectory bullet", fileName = "TrajectoryBullet", order = 51)]
    public class TrajectoryBullet : ScriptableObject
    {
        [SerializeField, Range(30, 200)] private int _maxCountPoints;
        [SerializeField, Range(5, 50)] private int _maxCountPointsAfterCollision;

        private Dictionary<int, IBulletTrajectoryPoint> _points;
        private int _countPointsBeforeCollision;
        private bool _isWasCollision;

        public Vector3 Direction { get; set; }
        public float DeltaTime { get; private set; }
        public bool IsFinished { get; private set; }

        public int MaxCountPoints => _maxCountPoints;
        public bool IsEmpty => _points == null || _points.Count == 0;

        public void CreateNewTrajectory(float deltaTime)
        {
            if (deltaTime <= 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime));

            DeltaTime = deltaTime;
            _points = new Dictionary<int, IBulletTrajectoryPoint>();
            _isWasCollision = false;

            IsFinished = false;
        }

        public void RecordCollision()
        {
            if (_isWasCollision) 
                return;

            _isWasCollision = true;
            _countPointsBeforeCollision = _points.Count;
        }

        public void AddPoint(BulletTrajectoryPoint point)
        {
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            _points.Add(point.Frame, point);

            if (_isWasCollision && _points.Count - _countPointsBeforeCollision >= _maxCountPointsAfterCollision)
                IsFinished = true;
        }

        public IBulletTrajectoryPoint GetPoint(int frame)
        {
            if (_points.ContainsKey(frame) == false) 
                throw new ArgumentOutOfRangeException(nameof(frame));

            return _points[frame];
        }

        public void DeleteAfterFrame(int frame)
        {
            if (_points.ContainsKey(frame) == false)
                throw new ArgumentOutOfRangeException(nameof(frame));

            var keysToRemove = _points.Keys.Where(key => key >= frame).ToList();

            foreach (var key in keysToRemove)
            {
                if (key >= frame)
                    _points.Remove(key);
            }
        }

        public Vector3[] GetPointsPosition()
        {
            return _points.Values.Select(p => p.Position).ToArray();
        }

        public void Clear()
        {
            if (_points == null)
                return;

            _points.Clear();
        }

        public bool HasFrame(int frame) => _points.ContainsKey(frame);
    }
}
