using CannonTurret.DamageSystem;
using System;

namespace CannonTurret.Effects
{
    public class PoisonDebuff : IDebuff
    {
        private const float MinGainFactor = 1f;
        private const int CountOperations = 3;

        private readonly IDamagedObject DamagedObject;

        private IDamageAttributes _damageAttributes;
        private int _currentOperation;

        public PoisonDebuff(IDamagedObject damagedObject, IDamageAttributes damageAttributes)
        {
            DamagedObject = damagedObject ?? throw new ArgumentNullException(nameof(damagedObject));
            _damageAttributes = damageAttributes ?? throw new ArgumentNullException(nameof(damageAttributes));
            _currentOperation = 0;
        }

        public DebuffType DebuffType => DebuffType.Poison;

        public bool IsExecutionCompleted => _currentOperation >= CountOperations;

        public void Activate()
        {
            DamagedObject.TakeDamage(_damageAttributes);
            _currentOperation++;
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