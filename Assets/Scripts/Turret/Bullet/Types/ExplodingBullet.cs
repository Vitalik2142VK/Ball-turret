using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet), typeof(Exploder), typeof(BulletPhysics))]
[RequireComponent(typeof(Renderer))]
public class ExplodingBullet : MonoBehaviour, IBullet, IInitializer
{
    [SerializeField] private Scriptable.DamageImproverAttributes _damageImproverAttributes;
    [SerializeField, SerializeIterface(typeof(IExplosionView))] private GameObject _explosionParticle;
    [SerializeField, Range(0.2f, 3f)] private float _waitTimeToPut = 0.5f;

    private IExploder _exploder;
    private IBulletPhysics _bulletPhysics;
    private IExplosionView _explosionView;
    private Bullet _bullet;
    private Renderer _renderer;
    private WaitForSeconds _wait;

    public ISound ExplosionSound { get; private set; }
    public IBulletRepository BulletRepository { get; private set; }

    public BulletType BulletType => _bullet.BulletType;

    private void OnValidate()
    {
        if (_damageImproverAttributes == null)
            throw new NullReferenceException(nameof(_damageImproverAttributes));

        if (_explosionParticle == null)
            throw new NullReferenceException(nameof(_explosionParticle));
    }

    private void Awake()
    {
        _bulletPhysics = GetComponent<IBulletPhysics>();
        _explosionView = _explosionParticle.GetComponent<IExplosionView>();
    }

    private void OnEnable()
    {
        _bulletPhysics.EnteredCollision += OnExplode;
    }

    private void OnDisable()
    {
        _bulletPhysics.EnteredCollision -= OnExplode;
    }

    public void Initialize()
    {
        if (_bullet != null && _renderer != null)
            return;

        _renderer = GetComponent<Renderer>();
        _bullet = GetComponent<Bullet>();
        _bullet.ChangeDamage(_damageImproverAttributes);

        _wait = new WaitForSeconds(_waitTimeToPut);
    }

    public void InitializeExploder(ISound explosionSound)
    {
        if (explosionSound == null)
            throw new ArgumentNullException(nameof(explosionSound));

        if (_bullet == null)
            Initialize();

        Exploder exploder = GetComponent<Exploder>();
        exploder.Initialize(_bullet.DamageAttributes, explosionSound, _explosionView);
        _exploder = exploder;
    }

    public void SetBulletRepository(IBulletRepository bulletRepository)
    {
        BulletRepository = bulletRepository ?? throw new ArgumentNullException(nameof(bulletRepository));
    }

    public void SetExplosionSound(ISound explosionSound)
    {
        ExplosionSound = explosionSound ?? throw new ArgumentNullException(nameof(explosionSound));
    }

    public void Move(Vector3 startPoint, Vector3 direction) => _bullet.Move(startPoint, direction);

    public void SetActive(bool isActive) => _bullet.SetActive(isActive);

    public void Gather(IBonus bonus) => _bullet.Gather(bonus);

    public bool TryGetBonuses(out IReadOnlyCollection<IBonus> bonuses) => _bullet.TryGetBonuses(out bonuses);

    private void OnExplode(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out IDamagedObject _))
        {
            _exploder.Explode(transform.position);

            StartCoroutine(HideBeforePut());
        }
    }

    private IEnumerator HideBeforePut()
    {
        _renderer.enabled = false;
        _bullet.enabled = false;

        yield return _wait;

        _renderer.enabled = true;
        _bullet.enabled = true;

        BulletRepository.Put(_bullet);
    }
}
