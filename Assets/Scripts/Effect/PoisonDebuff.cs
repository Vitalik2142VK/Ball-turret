public class PoisonDebuff : IDebuff
{
    private const int CountOperations = 3;

    private IDamageAttributes _damageAttributes;
    private IDamagedObject _damagedObject;
    private int _currentOperation;

    public PoisonDebuff(IDamagedObject damagedObject, IDamageAttributes damageAttributes)
    {
        _damagedObject = damagedObject ?? throw new System.ArgumentNullException(nameof(damagedObject));
        _damageAttributes = damageAttributes ?? throw new System.ArgumentNullException(nameof(damageAttributes));
        _currentOperation = 0;
    }

    public bool IsExecutionCompleted => _currentOperation >= CountOperations;

    public DebuffType DebuffType => DebuffType.Poison;

    public void Activate()
    {
        _damagedObject.TakeDamage(_damageAttributes);
        _currentOperation++;
    }
}