using CannonTurret.LevelSystem;
using CannonTurret.StepSystem;
using CannonTurret.UI.Animations;
using System;
using System.Collections;
using UnityEngine;

namespace CannonTurret.UI.PlayerScene
{
    [RequireComponent(typeof(ScaleAnimatorUI))]
    public class PauseMenu : MonoBehaviour, IWindow
    {
        [SerializeField] private Pause _pause;
        [SerializeField] private SettingMenu _settingMenu;

        private IChangeSceneStep _changeSceneStep;
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

        public void Initialize(IChangeSceneStep changeSceneStep)
        {
            _changeSceneStep = changeSceneStep ?? throw new ArgumentNullException(nameof(changeSceneStep));
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
            MainMenuLoader mainMenuLoader = new MainMenuLoader();

            _changeSceneStep.SetSceneLoader(mainMenuLoader);
            _changeSceneStep.Action();
        }

        private IEnumerator WaitClosure()
        {
            yield return _animator.GetYieldAnimation();

            gameObject.SetActive(false);
            _pause.Disable();
        }
    }
}