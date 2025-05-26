using UnityEngine;

//todo Remove late (for test)
public class CashUserService : IUserService
{
    private IImprovementTurretAttributes _turretImproverAttributes;

    public CashUserService(IImprovementTurretAttributes turretImproverAttributes)
    {
        _turretImproverAttributes = turretImproverAttributes ?? throw new System.ArgumentNullException(nameof(turretImproverAttributes));
    }

    public void Save(IUser user)
    {
        Debug.Log("User saved");
    }

    public IUser Load()
    {
        int coins = 1_000;
        Wallet wallet = new Wallet(coins);
        ITurretImprover turretImprover = new TurretImprover(_turretImproverAttributes);

        return new User(turretImprover, wallet);
    }
}