using System;

public interface IHealth : IDamagedObject
{
    public event Action Died;

    public void Restore();
}
