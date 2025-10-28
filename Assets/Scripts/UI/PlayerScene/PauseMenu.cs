using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ScaleAnimatorUI))]
public class PauseMenu : MonoBehaviour, IWindow
{
    [SerializeField] private Pause _pause;
    [SerializeField] private SettingMenu _settingMenu;

    private IStep _closeSceneStep;
    private IAnimatorUI _animator;

    private void OnValidate()
    {
        if (_pause == null)
            throw new NullReferenceException(nameof(_pause));

        if (_settingMenu == null)
            throw new NullReferenceException(nameof(_settingMenu));
    }
    
    private void Awake()
    {
        _animator = GetComponent<IAnimatorUI>();

        gameObject.SetActive(false);
    }

    public void Initialize(IStep closeSceneStep)
    {
        _closeSceneStep = closeSceneStep ?? throw new ArgumentNullException(nameof(closeSceneStep)); 
    }
        
    public void Enable()
    {
        gameObject.SetActive(true);
        _pause.Enable();
        _animator.Show();
    }

    public void OnPlay()
    {
        _animator.Hide();

        StartCoroutine(WaitClosure());
    }

    public void OnOpenSettingMenu()
    {
        gameObject.SetActive(false);
        _settingMenu.Open(this);
    }

    public void OnExit()
    {
        _closeSceneStep.Action();
    }

    private IEnumerator WaitClosure()
    {
        yield return _animator.GetYieldAnimation();

        gameObject.SetActive(false);
        _pause.Disable();
    }
}