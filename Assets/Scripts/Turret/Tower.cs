using UnityEngine;

[RequireComponent(typeof(DefaultGun))]
public class Tower : MonoBehaviour
{
    private IGun _gun;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;

        _gun = GetComponent<DefaultGun>();
    }

    public void SetTargertPosition(Vector3 targertPosition)
    {
        _transform.LookAt(targertPosition);
    }

    public void Shoot()
    {
        _gun.Shoot(_transform.forward);
    }
}
