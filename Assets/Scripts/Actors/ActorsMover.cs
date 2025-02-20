using System;
using System.Collections.Generic;
using UnityEngine;

public class ActorsMover : MonoBehaviour, IActorsMover
{
    [SerializeField] private Vector3 _distance;
    [SerializeField] private float _speed;

    private List<IMovableObject> _movableObjects;

    public bool AreMovesFinished { get; private set; }

    private void Start()
    {
        _movableObjects = new List<IMovableObject>();
    }

    public void SetMovableObject(IEnumerable<IMovableObject> movableObjects)
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
            mover.SetParameters(_distance, _speed);
        }
    }
}
