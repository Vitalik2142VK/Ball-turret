using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(Mover))]
public class BorderView : MonoBehaviour, IBorderView
{
    private IBorderPresenter _borderPresenter;
    private ISound _destroySound;

    public string Name => name;

    public IMover Mover { get; private set; }

    private void Awake()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;

        Mover = GetComponent<Mover>();
    }

    public void Initialize(IBorderPresenter borderPresenter, ISound destroySound)
    {
        _borderPresenter = borderPresenter ?? throw new ArgumentNullException(nameof(borderPresenter));
        _destroySound = destroySound ?? throw new ArgumentNullException(nameof(destroySound));
    }

    public void TakeDamage(IDamageAttributes damage)
    {
        throw new NotImplementedException();
    }

    public void IgnoreArmor(IDamageAttributes damage)
    {
        throw new NotImplementedException();
    }

    public void SetStartPosition(Vector3 startPosition)
    {
        throw new NotImplementedException();
    }

    public void Destroy()
    {
        _destroySound.Play();
        Destroy(gameObject);
    }
}
