using System;
using UnityEngine;

namespace RecorderLevel
{
    [RequireComponent(typeof(EnemyView))]
    public class RecordingEnemyPresenter : MonoBehaviour, IEnemyPresenter, IMovableObject
    {
        [SerializeField] private ActorAudioController _audioController;
        [SerializeField] private EnemyView _enemyView;

        private IEnemyView _view;
        private IMovableObject _mover;

        public bool IsFinished => _mover.IsFinished;

        private void OnValidate()
        {
            if (_audioController == null)
                throw new NullReferenceException(nameof(_audioController));

            if (_enemyView == null)
                _enemyView = GetComponent<EnemyView>();

            if (_enemyView == null)
                throw new NullReferenceException(nameof(_enemyView));
        }

        private void Awake()
        {
            _enemyView.Initialize(this, _audioController);

            _view = _enemyView;
            _mover = new Mover(transform);
        }

        public void AddDebuff(IDebuff debaff)
        {
            Debug.Log($"Add debuff: {debaff.DebuffType}");
        }

        public void PrepareAttacked(IAttackingEnemiesCollector attackingCollector)
        {
            Debug.Log($"PrepareAttacked");
        }

        public void PrepareDeleted(IRemovedActorsCollector removedCollector)
        {
            Debug.Log($"PrepareDeleted");
        }

        public void Destroy()
        {
            if (_view.IsActive)
                _view.PlayDead();
            else
                gameObject.SetActive(false);
        }

        public void TakeDamage(IDamageAttributes damage)
        {
            Destroy();
        }

        public void SetStartPosition(Vector3 startPosition) => _mover.SetStartPosition(startPosition);

        public void EstablishPoint(Vector3 distance, float speed) => _mover.EstablishPoint(distance, speed);

        public void Move()
        {
            _mover.Move();
            _view.PlayMovement(IsFinished == false);
        }

        public void Win() => _view.PlayVictory();
    }
}
