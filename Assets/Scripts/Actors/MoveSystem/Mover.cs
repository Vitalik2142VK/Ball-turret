using CannonTurret.Utils;
using System;
using UnityEngine;

namespace CannonTurret.Actors.MoveSystem
{
    public class Mover : IMovableObject
    {
        private readonly Transform Transform;

        private Vector3 _point;
        private float _speed;

        public bool IsFinished { get; private set; }

        public Mover(Transform movingObject)
        {
            if (movingObject == null)
                throw new ArgumentNullException(nameof(movingObject));

            Transform = movingObject;

            IsFinished = true;
        }

        public void SetStartPosition(Vector3 startPosition)
        {
            float y = Transform.position.y;
            Transform.position = new Vector3(startPosition.x, y, startPosition.z);
        }

        public void EstablishPoint(Vector3 distance, float speed)
        {
            if (speed <= 0f)
                throw new ArgumentOutOfRangeException();

            _point = Transform.position + distance;
            _speed = speed;

            IsFinished = false;
        }

        public void Move()
        {
            if (IsFinished)
                return;

            if (VectorTools.AreVectorsClose(Transform.position, _point) == false)
            {
                Transform.position = Vector3.MoveTowards(Transform.position, _point, _speed * Time.deltaTime);
            }
            else
            {
                Transform.position = _point;

                IsFinished = true;
            }
        }
    }
}