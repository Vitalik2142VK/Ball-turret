using CannonTurret.Turrets.Shooters;
using System;
using UnityEngine;

namespace CannonTurret.Turrets
{
    public class DestroyedTurretView : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _turretMeshRenderer;
        [SerializeField] private Tower _tower;
        [SerializeField] private ParticleSystem _destroyParticles;
        [SerializeField] private ShooterLose _shooterLose;

        private void OnValidate()
        {
            if (_turretMeshRenderer == null)
                throw new NullReferenceException(nameof(_turretMeshRenderer));

            if (_tower == null)
                throw new NullReferenceException(nameof(_tower));

            if (_destroyParticles == null)
                throw new NullReferenceException(nameof(_destroyParticles));

            if (_shooterLose == null)
                throw new NullReferenceException(nameof(_shooterLose));
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            _turretMeshRenderer.enabled = false;
            gameObject.SetActive(true);
            transform.rotation = _tower.transform.rotation;
            _destroyParticles.Play();
            _shooterLose.RunAway();
        }
    }
}