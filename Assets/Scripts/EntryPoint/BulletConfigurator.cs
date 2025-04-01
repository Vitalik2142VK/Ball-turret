using System;
using UnityEngine;

public class BulletConfigurator : MonoBehaviour
{
    [SerializeField] private BulletFactory _bulletFactory;

    private void OnValidate()
    {
        if (_bulletFactory == null)
            throw new NullReferenceException(nameof(_bulletFactory));
    }

    public void Configure(Player player)
    {
        if (player == null)
            throw new ArgumentNullException(nameof(player));

        IDamageImprover damageImprover = new DamageImprover(player.DamageImprover);
        _bulletFactory.Initialize(damageImprover);
    }
}
