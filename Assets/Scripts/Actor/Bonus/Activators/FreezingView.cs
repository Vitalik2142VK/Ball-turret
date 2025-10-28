using System;
using System.Collections;
using UnityEngine;

public class FreezingView : MonoBehaviour, IBonusActicatorView
{
    [SerializeField, SerializeIterface(typeof(IAnimatorUI))] private GameObject _imageFreeze;
    [SerializeField] private Sound _soundFreeze;

    private IAnimatorUI _animator;

    private void OnValidate()
    {
        if (_imageFreeze == null)
            throw new NullReferenceException(nameof(_imageFreeze));

        if (_soundFreeze == null)
            throw new NullReferenceException(nameof(_soundFreeze));
    }

    private void Awake()
    {
        _animator = _imageFreeze.GetComponent<IAnimatorUI>();
        _imageFreeze.SetActive(false);
    }

    public void PlayActivation()
    {
        _imageFreeze.SetActive(true);
        _animator.Show();
        _soundFreeze.Play();

        StartCoroutine(WaitOpening());
    }

    private IEnumerator WaitOpening()
    {
        yield return _animator.GetYieldAnimation();

        _animator.Hide();

        StartCoroutine(WaitClosure());
    }

    private IEnumerator WaitClosure()
    {
        yield return _animator.GetYieldAnimation();

        _imageFreeze.SetActive(false);
    }
}
