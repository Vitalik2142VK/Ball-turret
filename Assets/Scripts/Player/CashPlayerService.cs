using UnityEngine;

//todo Remove late (for test)
public class CashPlayerService : IPlayerService
{
    private IImprovementTurretAttributes _turretImproverAttributes;

    public CashPlayerService(IImprovementTurretAttributes turretImproverAttributes)
    {
        _turretImproverAttributes = turretImproverAttributes ?? throw new System.ArgumentNullException(nameof(turretImproverAttributes));
    }

    public void Save(IPlayer user)
    {
        Debug.Log("User saved");
    }

    public IPlayer Load()
    {
        int coins = 1_000;
        Wallet wallet = new Wallet(coins);
        ITurretImprover turretImprover = new TurretImprover(_turretImproverAttributes);

        return new Player(turretImprover, wallet);
    }
}