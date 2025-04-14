using System;
using UnityEngine;

public class FireBulletDebuff : MonoBehaviour, IBulletDebuff
{
    private const float FireDamageCoefficient = 1.5f;

    private IDamageAttributes _damageAttributes;

    public void Initialize(IDamageAttributes attributes)
    {
        if (attributes == null)
            throw new ArgumentNullException(nameof(attributes));

        _damageAttributes = new DamageAttributes(attributes.Damage * FireDamageCoefficient);
    }

    public void ApplyDebuff(IDebuffable debuffsReceiver)
    {
        if (debuffsReceiver == null)
            throw new ArgumentNullException(nameof(debuffsReceiver));

        if (debuffsReceiver is IDamagedObject damagedObject)
        {
            FireDebuff fireDebuff = new FireDebuff(damagedObject, _damageAttributes);
            debuffsReceiver.AddDebuff(fireDebuff);
        }
    }
}
