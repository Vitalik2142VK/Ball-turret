using UnityEngine;

//todo Remove late (for test)
public class CashUserService : IUserService
{
    private ITurretImprover _turretImprover;

    public CashUserService(ITurretImprover turretImprover)
    {
        _turretImprover = turretImprover ?? throw new System.ArgumentNullException(nameof(turretImprover));
    }

    public void Save(IUser user)
    {
        Debug.Log("User saved");
    }

    public IUser Load()
    {
        int coins = 1000;
        Wallet wallet = new Wallet(coins);

        return new User(_turretImprover, wallet);
    }
}