public class FireDebuff : IDebuff
{
    private IDamageAttributes _damageAttributes;
    private IDamagedObject _damagedObject;

    public FireDebuff(IDamagedObject damagedObject, IDamageAttributes damageAttributes)
    {
        _damagedObject = damagedObject ?? throw new System.ArgumentNullException(nameof(damagedObject));
        _damageAttributes = damageAttributes ?? throw new System.ArgumentNullException(nameof(damageAttributes));

        IsExecutionCompleted = false;
    }

    public bool IsExecutionCompleted { get; private set; }

    public DebuffType DebuffType => DebuffType.Fire;

    public void Activate()
    {
        _damagedObject.TakeDamage(_damageAttributes);

        IsExecutionCompleted = true;
    }
}
