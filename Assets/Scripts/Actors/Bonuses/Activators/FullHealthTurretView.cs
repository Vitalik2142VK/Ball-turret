using CannonTurret.AudioSystem;
using System;
using UnityEngine;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class FullHealthTurretView : MonoBehaviour, IBonusActicatorView
    {
        [SerializeField] private ParticleSystem _fullTurretParticle;
        [SerializeField] private Sound _turretRepairSound;

        private void OnValidate()
        {
            if (_fullTurretParticle == null)
                throw new NullReferenceException(nameof(_fullTurretParticle));

            if (_turretRepairSound == null)
                throw new NullReferenceException(nameof(_turretRepairSound));
        }

        public void PlayActivation()
        {
            _fullTurretParticle.Play();
            _turretRepairSound.Play();
        }
    }
}