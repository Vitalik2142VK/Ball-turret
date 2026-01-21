using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ScaleAnimatorUI), typeof(CanvasGroup))]
public class PauseMenu : MonoBehaviour, IWindow
{
    [SerializeField] private Pause _pause;
    [SerializeField] private SettingMenu _settingMenu;

    private IChangeSceneStep _changeSceneStep;
    private IAnimatorUI _animator;
    private CanvasGroup _canvasGroup;

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
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }

    public void Initialize(IChangeSceneStep changeSceneStep)
    {
        _changeSceneStep = changeSceneStep ?? throw new ArgumentNullException(nameof(changeSceneStep)); 
    }
        
    public void Enable()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
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
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _settingMenu.Open(this);
    }

    public void OnExit()
    {
        MainMenuLoader mainMenuLoader = new MainMenuLoader();

        _changeSceneStep.SetSceneLoader(mainMenuLoader);
        _changeSceneStep.Action();
    }

    private IEnumerator WaitClosure()
    {
        yield return _animator.GetYieldAnimation();

        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _pause.Disable();
    }
}