using System;
using UnityEngine;

namespace CannonTurret.Clip
{
    public class RandomRecordingEnemy : MonoBehaviour
    {
        [SerializeField] private RecordedEnemyView[] _enemies;

        private RecordedEnemyView _currentEnemy;

        private void OnValidate()
        {
            if (_enemies == null || _enemies.Length == 0)
                throw new InvalidOperationException(nameof(_enemies));

            foreach (var enemy in _enemies)
                if (enemy == null)
                    throw new NullReferenceException($"{_enemies} contains null objects");
        }

        private void Awake()
        {
            foreach (var enemy in _enemies)
                enemy.SetActive(false);
        }

        private void OnEnable()
        {
            int randomIndex = UnityEngine.Random.Range(0, _enemies.Length);
            _currentEnemy = _enemies[randomIndex];
            _currentEnemy.SetActive(true);
        }

        private void OnDisable()
        {
            _currentEnemy.SetActive(false);
        }

        public void PlayDead()
        {
            _currentEnemy.PlayDead();
        }
    }
}