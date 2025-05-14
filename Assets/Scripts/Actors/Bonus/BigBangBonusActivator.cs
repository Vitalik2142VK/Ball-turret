using System;
using UnityEngine;

public class BigBangBonusActivator : IBonusActivator
{
    private IExploder _exploder;
    private Vector3 _pointExplosionPosition;

    public BigBangBonusActivator(IExploder exploder, Vector3 pointExplosionPosition)
    {
        _exploder = exploder ?? throw new ArgumentNullException(nameof(exploder));
        _pointExplosionPosition = pointExplosionPosition;
    }

    public void Activate()
    {
        _exploder.Explode(_pointExplosionPosition);
    }

    public IBonusActivator Clone()
    {
        return new BigBangBonusActivator(_exploder, _pointExplosionPosition);
    }
}