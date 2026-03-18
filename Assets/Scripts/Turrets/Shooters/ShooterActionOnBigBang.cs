using CannonTurret.Effects;
using System;
using UnityEngine;

namespace CannonTurret.Turrets.Shooters
{
    [RequireComponent(typeof(ShooterView))]
    public class ShooterActionOnBigBang : MonoBehaviour
    {
        [SerializeField] private RocketView _rocketView;

        private ShooterView _shooterView;

        private void OnValidate()
        {
            if (_rocketView == null)
                throw new NullReferenceException(nameof(_rocketView));
        }

        private void Awake()
        {
            _shooterView = GetComponent<ShooterView>();
        }

        private void OnEnable()
        {
            _rocketView.RocketFinished += OnPlayTakeCover;
        }

        private void OnDisable()
        {
            _rocketView.RocketFinished -= OnPlayTakeCover;
        }

        private void OnPlayTakeCover() => _shooterView.PlayTakeCover();
    }
}