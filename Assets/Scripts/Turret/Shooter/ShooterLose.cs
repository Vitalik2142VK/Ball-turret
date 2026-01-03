using System;
using UnityEngine;

public class ShooterLose : MonoBehaviour
{
    [SerializeField] private ShooterMover _shooterMover;
    [SerializeField] private ShooterView _shooterView;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _finishPoint;

    private void OnValidate()
    {
        if (_shooterMover == null)
            throw new NullReferenceException(nameof(_shooterMover));

        if (_shooterView == null)
            throw new NullReferenceException(nameof(_shooterView));

        if (_startPoint == null)
            throw new NullReferenceException(nameof(_startPoint));

        if (_finishPoint == null)
            throw new NullReferenceException(nameof(_finishPoint));
    }

    public void RunAway()
    {
        _shooterView.transform.parent = null;
        _shooterMover.SetStartPosition(_startPoint.position);
        _shooterMover.MoveTo(_finishPoint.position);
    }
}
