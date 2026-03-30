using CannonTurret.DamageSystem;
using System;

namespace CannonTurret.Effects
{
    public class FireDebuff : IDebuff
    {
        private const float MinGainFactor = 1f;

        private readonly IDamagedObject _damagedObject;

        private IDamageAttributes _damageAttributes;

        public FireDebuff(IDamagedObject damagedObject, IDamageAttributes damageAttributes)
        {
            _damagedObject = damagedObject ?? throw new ArgumentNullException(nameof(damagedObject));
            _damageAttributes = damageAttributes ?? throw new ArgumentNullException(nameof(damageAttributes));

            IsExecutionCompleted = false;
        }

        public DebuffType DebuffType => DebuffType.Fire;

        public bool IsExecutionCompleted { get; private set; }

        public void Activate()
        {
            _damagedObject.TakeDamage(_damageAttributes);

            IsExecutionCompleted = true;
        }

        public void Strengthen(float gainFactor)
        {
            if (gainFactor < MinGainFactor)
                throw new ArgumentOutOfRangeException(nameof(gainFactor));

            var damageChanger = new DamageChanger(_damageAttributes);
            damageChanger.Change(gainFactor);
            _damageAttributes = damageChanger;
        }
    }
}