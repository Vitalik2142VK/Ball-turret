using CannonTurret.Effects;
using System;
using UnityEngine;

namespace CannonTurret.Actors.Bonuses.Activators
{
    public class BigBangBonusActivator : IBonusActivator
    {
        private readonly IExploder Exploder;
        private readonly IEnemyCounter EnemyCounter;

        private Vector3 _pointExplosionPosition;

        public BigBangBonusActivator(IExploder exploder, IEnemyCounter enemyCounter, Vector3 pointExplosionPosition)
        {
            Exploder = exploder ?? throw new ArgumentNullException(nameof(exploder));
            EnemyCounter = enemyCounter ?? throw new ArgumentNullException(nameof(enemyCounter));
            _pointExplosionPosition = pointExplosionPosition;
        }

        public void Activate()
        {
            Exploder.Explode(_pointExplosionPosition);
            EnemyCounter.Count();
        }
    }
}