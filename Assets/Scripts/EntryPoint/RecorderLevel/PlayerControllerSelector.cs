using RecorderLevel;
using System;
using UnityEngine;

public class PlayerControllerSelector : MonoBehaviour, IPlayerController
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AIPlayerController _aiPlayerController;

    private IPlayerController _currentPlayerController;

    private void OnValidate()
    {
        if (_playerController == null)
            throw new NullReferenceException(nameof(_playerController));

        if (_aiPlayerController == null)
            throw new NullReferenceException(nameof(_aiPlayerController));
    }

    private void OnEnable()
    {
        //_aiPlayerController.Shoted += OnSelectPlayerController;
    }

    private void OnDisable()
    {
        //_aiPlayerController.Shoted -= OnSelectPlayerController;
    }

    public void Initialize(ITurret turret)
    {
        _playerController.Initialize(turret);
        _aiPlayerController.Initialize(turret);
        _currentPlayerController = _playerController;
    }

    public void SelectTarget()
    {
        SwitchActivity();

        _currentPlayerController.SelectTarget();
    }

    private void SwitchActivity()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (_currentPlayerController == _playerController)
                _currentPlayerController = _aiPlayerController;
            else
                _currentPlayerController = _playerController;
        }
    }

    private void OnSelectPlayerController()
    {
        _currentPlayerController = _playerController;
    }
}