using System;
using System.Collections.Generic;

namespace CannonTurret.Actors.MoveSystem
{
    public class ActorsMover : IAdvancedActorsMover
    {
        private readonly List<IMovableObject> MovableObjects;

        private IMoveAttributes _moveAttributes;

        public ActorsMover()
        {
            MovableObjects = new List<IMovableObject>();
        }

        public bool AreMovesFinished { get; private set; }

        public void SetMoveAttributes(IMoveAttributes moveAttributes)
        {
            _moveAttributes = moveAttributes ?? throw new ArgumentNullException(nameof(moveAttributes));
        }

        public void SetMovableObjects(IEnumerable<IMovableObject> movableObjects)
        {
            if (movableObjects == null)
                throw new ArgumentNullException(nameof(movableObjects));

            if (MovableObjects.Count != 0)
                MovableObjects.Clear();

            MovableObjects.AddRange(movableObjects);

            SpecifyNewPosition();
        }

        public void MoveAll()
        {
            Move();

            if (AreMovesFinished)
            {
                SpecifyNewPosition();

                MovableObjects.Clear();
            }
        }

        private void Move()
        {
            AreMovesFinished = true;

            foreach (var movableObject in MovableObjects)
            {
                movableObject.Move();

                if (AreMovesFinished && movableObject.IsFinished == false)
                    AreMovesFinished = false;
            }
        }

        private void SpecifyNewPosition()
        {
            foreach (var movableObject in MovableObjects)
                movableObject.EstablishPoint(_moveAttributes.Distance, _moveAttributes.Speed);
        }
    }
}