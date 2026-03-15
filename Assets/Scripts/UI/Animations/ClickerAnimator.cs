using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ClickerAnimator : MonoBehaviour
{
    private const string IsVertival = nameof(IsVertival);

    private ICameraAdapter _cameraAdapter;
    private Animator _animator;
    private int _hashIsVertival;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        Camera camera = Camera.main;

        if (camera.TryGetComponent(out ICameraAdapter cameraAdapter) == false)
            throw new System.InvalidOperationException($"The main camera does not contain the component: <{nameof(ICameraAdapter)}>");

        _cameraAdapter = cameraAdapter;
        _hashIsVertival = Animator.StringToHash(IsVertival);
    }

    private void OnEnable()
    {
        _cameraAdapter.OrientationChanged += OnChangeAnimation;
    }


    private void Start()
    {
        OnChangeAnimation();
    }

    private void OnDisable()
    {
        _cameraAdapter.OrientationChanged -= OnChangeAnimation;
    }

    private void OnChangeAnimation()
    {
        _animator.SetBool(_hashIsVertival, _cameraAdapter.IsPortraitOrientation);
    }
}