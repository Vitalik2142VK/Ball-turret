using UnityEngine;

[CreateAssetMenu(menuName = "Attributes/Damage Attributes", fileName = "DamageAttributes", order = 51)]
public class DamageAttributes : ScriptableObject, IDamageAttributes
{
    [SerializeField, Min(1)] private int _damage;
    
    public int Damage => _damage;
}
