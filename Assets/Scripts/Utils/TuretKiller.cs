using UnityEngine;

//todo Remove on realise
public class TuretKiller : MonoBehaviour
{
    [SerializeField] private PlayLevel.TurretConfigurator _turretConfigurator;
    [SerializeField] private KeyCode _killKey = KeyCode.T;

    private IDamage _damage;

    private void OnValidate()
    {
        if (_turretConfigurator == null)
            throw new System.NullReferenceException(nameof(_turretConfigurator));
    }

    private void Awake()
    {
        DamageAttributes damageAttributes = new DamageAttributes(int.MaxValue);
        _damage = new Damage(damageAttributes);
    }

    private void Update()
    {
        if (Input.GetKeyUp(_killKey))
            KillTurret();
    }

    private void KillTurret()
    {
        var turret = _turretConfigurator.Turret;
        _damage.Apply(turret);
    }
}
