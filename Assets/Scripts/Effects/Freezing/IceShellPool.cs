using CannonTurret.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CannonTurret.Effects.Freezing
{
    public class IceShellPool : MonoBehaviour, IIceShellPool
    {
        [SerializeField] private IceShell _iceShellPrefab;

        private ObjectsPool<IceShell> _pool;
        private HashSet<IceShell> _activeIceShells;

        private void Awake()
        {
            _pool = new ObjectsPool<IceShell>(transform, _iceShellPrefab);
            _activeIceShells = new HashSet<IceShell>();
        }

        public IIceShell Get()
        {
            var iceShell = _pool.GetGameObject();
            iceShell.Initialize(this);
            _activeIceShells.Add(iceShell);

            return iceShell;
        }

        public void Put(IIceShell shell)
        {
            if (shell == null)
                throw new ArgumentNullException(nameof(shell));

            if (shell is IceShell iceShell == false)
                throw new ArgumentException(nameof(shell));

            if (_activeIceShells.Contains(iceShell) == false)
                throw new ArgumentException($"{nameof(_activeIceShells)} does not contain {nameof(shell)}");

            _activeIceShells.Remove(iceShell);
            _pool.PutGameObject(iceShell);
        }

        public void DisableAll()
        {
            foreach (var iceShell in _activeIceShells)
                iceShell.Disable();
        }
    }
}