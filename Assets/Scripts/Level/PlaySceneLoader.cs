using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneLoader : MonoBehaviour, ISceneLoader
{
    private string NameScene = "PlayScene";

    [SerializeField] private Scriptable.SelectedLevel _selectedLevel;

    private void OnValidate()
    {
        if (_selectedLevel == null)
            throw new NullReferenceException(nameof(_selectedLevel));
    }

    public void SetSelectedLevel(ILevel level)
    {
        if (level == null)
            throw new ArgumentNullException(nameof(level));

        _selectedLevel.Initialize(level);
    }

    public void Load()
    {
        SceneManager.LoadScene(NameScene);
    }
}
