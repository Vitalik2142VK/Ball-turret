using UnityEngine;

[CreateAssetMenu(menuName = "Attributes/Health Attributes", fileName = "HealthAttributes", order = 51)]
public class HealthAttributes : ScriptableObject, IHealthAttributes
{
    [SerializeField, Min(10)] private int _maxHealth;

    public int MaxHealth => _maxHealth;
}
