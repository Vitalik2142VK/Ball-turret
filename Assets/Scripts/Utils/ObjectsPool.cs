using System;
using System.Collections.Generic;
using UnityEngine;

namespace CannonTurret.Utils
{
    public class ObjectsPool<T> where T : MonoBehaviour
    {
        private Stack<T> _pool;
        private Transform _conteiner;
        private T _prefab;

        public ObjectsPool(Transform conteiner, T prefab)
        {
            if (conteiner == null)
                throw new ArgumentNullException(nameof(conteiner));

            _conteiner = conteiner;
            _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));

            _pool = new Stack<T>();
        }

        public T GetGameObject()
        {
            T obj;

            if (_pool.Count == 0)
            {
                obj = UnityEngine.Object.Instantiate(_prefab);
                obj.transform.parent = _conteiner;
            }
            else
            {
                obj = _pool.Pop();
            }

            obj.gameObject.SetActive(true);

            return obj;
        }

        public void PutGameObject(T gameObject)
        {
            if (gameObject == null)
                throw new ArgumentNullException(nameof(gameObject));

            gameObject.transform.parent = _conteiner;
            gameObject.gameObject.SetActive(false);
            _pool.Push(gameObject);
        }

        public void Clear()
        {
            for (int i = 0; i < _conteiner.childCount; i++)
                if (_conteiner.GetChild(i).gameObject.TryGetComponent(out T component))
                    UnityEngine.Object.Destroy(component.gameObject);

            _pool.Clear();
        }
    }
}