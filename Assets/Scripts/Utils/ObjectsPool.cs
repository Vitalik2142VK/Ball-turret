using System;
using System.Collections.Generic;
using UnityEngine;

namespace CannonTurret.Utils
{
    public class ObjectsPool<T> where T : MonoBehaviour
    {
        private readonly Stack<T> Pool;
        private readonly Transform Conteiner;
        private readonly T Prefab;

        public ObjectsPool(Transform conteiner, T prefab)
        {
            if (conteiner == null)
                throw new ArgumentNullException(nameof(conteiner));

            Conteiner = conteiner;
            Prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));

            Pool = new Stack<T>();
        }

        public T GetGameObject()
        {
            T obj;

            if (Pool.Count == 0)
            {
                obj = UnityEngine.Object.Instantiate(Prefab);
                obj.transform.parent = Conteiner;
            }
            else
            {
                obj = Pool.Pop();
            }

            obj.gameObject.SetActive(true);

            return obj;
        }

        public void PutGameObject(T gameObject)
        {
            if (gameObject == null)
                throw new ArgumentNullException(nameof(gameObject));

            gameObject.transform.parent = Conteiner;
            gameObject.gameObject.SetActive(false);
            Pool.Push(gameObject);
        }

        public void Clear()
        {
            for (int i = 0; i < Conteiner.childCount; i++)
                if (Conteiner.GetChild(i).gameObject.TryGetComponent(out T component))
                    UnityEngine.Object.Destroy(component.gameObject);

            Pool.Clear();
        }
    }
}