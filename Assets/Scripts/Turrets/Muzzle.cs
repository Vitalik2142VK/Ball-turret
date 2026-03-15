using UnityEngine;

public class Muzzle : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPoint;

    private Transform _transform;

    public Vector3 BulletSpawnPoint => _bulletSpawnPoint.position;
    public Vector3 Position => _transform.position;
    public Vector3 Direction => _transform.forward;

    private void OnValidate()
    {
        if (_bulletSpawnPoint == null)
            throw new System.NullReferenceException(nameof(_bulletSpawnPoint));
    }

    private void Awake()
    {
        _transform = transform;
    }

    public void LookAtTarget(Vector3 localTargetPosition)
    {
        localTargetPosition = new Vector3(localTargetPosition.x, _transform.position.y, localTargetPosition.z);
        float angleRotaion = Mathf.Atan2(localTargetPosition.y, localTargetPosition.z) * Mathf.Rad2Deg;
        _transform.localRotation = Quaternion.Euler(angleRotaion, 0f, 0f);
    }
}
