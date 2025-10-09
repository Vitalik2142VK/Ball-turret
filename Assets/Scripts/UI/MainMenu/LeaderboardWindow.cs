using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HiderUI), typeof(ShiftAnimatorUI))]
public class LeaderboardWindow : MonoBehaviour
{
    private IMenu _previousMenu;
    private IAnimatorUI _animator;
    private HiderUI _hiderUI;

    private void Awake()
    {
        _hiderUI = GetComponent<HiderUI>();
        _animator = GetComponent<ShiftAnimatorUI>();

        gameObject.SetActive(false);
    }

    public void OnClose()
    {
        _hiderUI.Enable();

        StartCoroutine(WaitClosure());
    }

    public void Open(IMenu previousMenu)
    {
        _previousMenu = previousMenu ?? throw new ArgumentNullException(nameof(previousMenu));

        gameObject.SetActive(true);
        _hiderUI.Disable();
        _animator.Show();
    }

    private IEnumerator WaitClosure()
    {
        yield return _animator.GetYieldAnimation();

        gameObject.SetActive(false);
        _previousMenu.Enable();
    }
}
