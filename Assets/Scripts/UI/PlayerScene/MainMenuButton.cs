using System;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    private IChangeSceneStep _changeSceneStep;

    public void Initialize(IChangeSceneStep changeSceneStep)
    {
        _changeSceneStep = changeSceneStep ?? throw new ArgumentNullException(nameof(changeSceneStep));
    }
}
