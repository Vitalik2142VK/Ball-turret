using System;
using UnityEngine;

namespace RecorderLevel
{
    public class AIPlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private Transform _touchPosition;
        [SerializeField] private AITarget _target;
        [SerializeField, Min(0.5f)] private float _aimSpeed = 1f;
        [SerializeField, Min(0.5f)] private float _waitFixTurrgetTime = 1f;

        private ITurret _turret;
        private Timer _waitFixTurrget;
        private float _aimDistance = 0.1f;

        public event Action Shoted;

        private void OnValidate()
        {
            if (_touchPosition == null)
                throw new NullReferenceException(nameof(_touchPosition));

            if (_target == null)
                throw new NullReferenceException(nameof(_target));
        }

        public void Initialize(ITurret turret)
        {
            _turret = turret ?? throw new ArgumentNullException(nameof(turret));

            _waitFixTurrget = new Timer(_waitFixTurrgetTime);
        }

        public void SelectTarget()
        {

            if (_turret.IsReadyShoot == false)
                return;

            UpdateAim();

            _turret.SetTouchPoint(_touchPosition.position);

            if (IsAimedTarget())
                WaitFixTouchPosition();
        }

        private bool IsAimedTarget() => Vector3.Distance(_touchPosition.position, _target.Position) < _aimDistance;

        private void UpdateAim()
        {
            if (_target.IsSelected == false)
            {
                _target.Select();
                _waitFixTurrget.UpdateWaitingTime();
            }

            Vector3 curretnTouchPosition = _touchPosition.position;

            _touchPosition.position = Vector3.MoveTowards(curretnTouchPosition, _target.Position, Time.deltaTime * _aimSpeed);
        }

        private void WaitFixTouchPosition()
        {
            if (_waitFixTurrget.IsTimeUp)
            {
                _target.ThrowOff();
                _turret.FixTargetPostion(_touchPosition.position);

                Shoted?.Invoke();
            }
            else
            {
                _waitFixTurrget.MakeCountdown(Time.deltaTime);
            }
        }
    }
}
