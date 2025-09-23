using System;
using UnityEngine;

[RequireComponent(typeof(Exploder))]
public class BigBangBonusActivatorCreator : MonoBehaviour, IBonusActivatorCreator
{
    [SerializeField] private Scriptable.CachedPlayer _player;
    [SerializeField] private Scriptable.DamageAttributes _explosionDamageAttributes;
    [SerializeField] private Transform _pointExplosion;
    [SerializeField] private Sound _bigBangSound;
    [SerializeField] private ExplosionView _explosionView;

    private void OnValidate()
    {
        if (_player == null)
            throw new NullReferenceException(nameof(_player));

        if (_pointExplosion == null)
            throw new NullReferenceException(nameof(_pointExplosion));

        if (_bigBangSound == null)
            throw new NullReferenceException(nameof(_bigBangSound));

        if (_explosionView == null)
            throw new NullReferenceException(nameof(_explosionView));

        if (_explosionDamageAttributes == null)
            throw new NullReferenceException(nameof(_explosionDamageAttributes));
    }

    public IBonusActivator Create()
    {
        Exploder exploder = GetComponent<Exploder>();
        DamageChanger damageChanger = new DamageChanger(_explosionDamageAttributes);
        float damageCoefficient = _player.DamageCoefficient;
        damageChanger.Change(damageCoefficient);
        exploder.Initialize(damageChanger, _bigBangSound, _explosionView);

        return new BigBangBonusActivator(exploder, _pointExplosion.position);
    }
}
