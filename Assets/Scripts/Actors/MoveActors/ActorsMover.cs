using System;
using System.Collections.Generic;
using UnityEngine;

public class ActorsMover : IActorsMover
{
    private List<IMovableObject> _movableObjects;
    private IMoveAttributes _moveAttributes;

    public ActorsMover()
    {
        _movableObjects = new List<IMovableObject>();
    }

    public bool AreMovesFinished { get; private set; }

    private void Start()
    {
    }

    public void SetMoveAttributes(IMoveAttributes moveAttributes)
    {
        _moveAttributes = moveAttributes ?? throw new ArgumentNullException(nameof(moveAttributes));
    }

    public void SetMovableObjects(IEnumerable<IMovableObject> movableObjects)
    {
        if (movableObjects == null)
            throw new ArgumentNullException(nameof(movableObjects));

        _movableObjects.AddRange(movableObjects);

        SpecifyNewPosition();
    }

    public void MoveAll()
    {
        Move();

        if (AreMovesFinished)
        {
            SpecifyNewPosition();

            _movableObjects.Clear();
        }
    }

    private void Move()
    {
        AreMovesFinished = true;

        foreach (IMovableObject movableObject in _movableObjects)
        {
            IMover mover = movableObject.Mover ?? throw new NullReferenceException(nameof(movableObject.Mover));
            mover.Move();

            if (AreMovesFinished && mover.IsFinished == false)
                AreMovesFinished = false;
        }
    }

    private void SpecifyNewPosition()
    {
        foreach (IMovableObject movableObject in _movableObjects)
        {
            IMover mover = movableObject.Mover ?? throw new NullReferenceException(nameof(movableObject.Mover));
            mover.SetParameters(_moveAttributes.Distance, _moveAttributes.Speed);
        }
    }
}
