using UnityEngine;

namespace CannonTurret.Effects.Freezing
{
    [RequireComponent(typeof(Animator))]
    public class FreesableObject : MonoBehaviour, IFreesableObject
    {
        private IIceShell _iceShell;
        private Animator _animator;
        private float _speedAnimation;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnDisable()
        {
            if (HasIceShell)
            {
                _iceShell.Disable();

                OnRemoveIceShell();
            }
        }

        public bool HasIceShell => _iceShell != null;

        public void Freeze(IIceShell iceShell)
        {
            if (HasIceShell)
                return;

            _iceShell = iceShell ?? throw new System.ArgumentNullException(nameof(iceShell));
            _iceShell.SetScale(transform.lossyScale);
            _iceShell.SetPosition(transform.position);
            _iceShell.Enable();
            _iceShell.Disabled += OnRemoveIceShell;

            _speedAnimation = _animator.speed;
            _animator.speed = 0;
        }

        private void OnRemoveIceShell()
        {
            if (HasIceShell == false)
                return;

            _animator.speed = _speedAnimation;
            _iceShell.Disabled -= OnRemoveIceShell;
            _iceShell = null;
        }
    }
}