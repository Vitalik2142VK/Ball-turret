using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class DynamicEnemyMover : MonoBehaviour
{
    private EnemyAnimator _enemyAnimator;
    private Mover _mover;

    private void Awake()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _mover = new Mover(transform);
        _mover.SetStartPosition(transform.position);
    }

    private void Start()
    {
        _enemyAnimator.PlayMovement(true);
    }

    private void Update()
    {
        if (_mover.IsFinished)
            EstablishNewPosition();
    }

    private void EstablishNewPosition()
    {
        throw new NotImplementedException();
    }
}
