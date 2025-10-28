using System;
using UnityEngine;

public class BigBangBonusActivator : IBonusActivator
{
    private IExploder _exploder;
    private IEnemyCounter _enemyCounter;
    private Vector3 _pointExplosionPosition;

    public BigBangBonusActivator(IExploder exploder, IEnemyCounter enemyCounter, Vector3 pointExplosionPosition)
    {
        _exploder = exploder ?? throw new ArgumentNullException(nameof(exploder));
        _enemyCounter = enemyCounter ?? throw new ArgumentNullException(nameof(enemyCounter));
        _pointExplosionPosition = pointExplosionPosition;
    }

    public void Activate()
    {
        _exploder.Explode(_pointExplosionPosition);
        _enemyCounter.Count();
    }
}