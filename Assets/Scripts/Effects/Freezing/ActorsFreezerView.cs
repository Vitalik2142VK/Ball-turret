using CannonTurret.Actors;
using System;
using UnityEngine;

namespace CannonTurret.Effects.Freezing
{
    public class ActorsFreezerView : MonoBehaviour, IActorsFreezerView
    {
        private const float HalfValue = 0.5f;

        [SerializeField] private IceShellPool _iceShellPool;
        [SerializeField] private ActorZone _actorZone;
        [SerializeField] private LayerMask _layerMask;

        private BoxCollider _box;

        private void OnValidate()
        {
            if (_iceShellPool == null)
                throw new NullReferenceException(nameof(_iceShellPool));

            if (_actorZone == null)
                throw new NullReferenceException(nameof(_actorZone));
        }

        private void Awake()
        {
            _box = _actorZone.GetComponent<BoxCollider>();
        }

        public void Defrost() => _iceShellPool.DisableAll();

        public void Freeze()
        {
            Vector3 center = _box.center;
            Vector3 halfExtents = Vector3.Scale(_box.size * HalfValue, _box.transform.lossyScale);
            Quaternion orientation = _box.transform.rotation;

            var colliders = Physics.OverlapBox(center, halfExtents, orientation, _layerMask);

            foreach (var collider in colliders)
                if (collider.TryGetComponent(out IFreesableObject freesableObject))
                    AppointIceShell(freesableObject);
        }

        private void AppointIceShell(IFreesableObject freesableObject)
        {
            if (freesableObject.HasIceShell)
                return;

            var iceShell = _iceShellPool.Get();
            freesableObject.Freeze(iceShell);
        }
    }
}