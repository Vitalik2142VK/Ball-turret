using CannonTurret.Scriptable.Level;
using CannonTurret.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CannonTurret.LevelSystem
{
    public class PlaySceneLoader : MonoBehaviour, ISceneLoader
    {
        [SerializeField] private SelectedLevel _selectedLevel;

        private void OnValidate()
        {
            if (_selectedLevel == null)
                throw new NullReferenceException(nameof(_selectedLevel));
        }

        public void SetSelectedLevel(ILevel level)
        {
            if (level == null)
                throw new ArgumentNullException(nameof(level));

            _selectedLevel.SetLevel(level);
        }

        public void Load()
        {
            if (_selectedLevel.HasLevel == false)
                throw new InvalidOperationException("The download level is not specified");

            SceneManager.LoadScene((int)SceneIndex.PlayScene);
        }
    }
}