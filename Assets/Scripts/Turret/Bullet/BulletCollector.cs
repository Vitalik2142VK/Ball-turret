using UnityEngine;

public class BulletCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IBullet bullet))
            bullet.EndFlight();
    }
}
