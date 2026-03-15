using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShooterView))]
public class ShooterMover : MonoBehaviour
{
    private const float MinSqrMagnitude = 0.001f;

    [SerializeField, Min(1f)] private float _runningTime = 1f;
    [SerializeField, Min(0.25f)] private float _timeWait = 0.25f;

    private Transform _transform;
    private ShooterView _shooterView;
    private TweenController _controller;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _transform = transform;
        _shooterView = GetComponent<ShooterView>();
        _controller = new TweenController();
    }

    private void OnDisable()
    {
        _controller.KillCurrentAnimation();
    }

    public void SetStartPosition(Vector3 startPosition)
    {
        _transform.position = startPosition;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;

        RotationToTarget();
        StartCoroutine(WaitMove());
    }

    private IEnumerator WaitMove()
    {
        yield return new WaitForSeconds(_timeWait);

        var animation = _transform.DOMove(_targetPosition, _runningTime);
        _controller.PlayAnimation(animation);
        _shooterView.PlayRunAway();
    }

    private void RotationToTarget()
    {
        Vector3 direction = _targetPosition - _transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > MinSqrMagnitude)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }
}