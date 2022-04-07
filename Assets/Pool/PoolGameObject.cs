using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer2D.Assets.Pool
{
    internal sealed class PoolGameObject
    {
        private Stack<GameObject> _stack;
        private GameObject _prefab;
        private Transform _root;
        private string _prefabGameName;

        public PoolGameObject(GameObject prefab, string prefabGameName, string poolName)
        {
            _prefab = prefab;
            _prefabGameName = prefabGameName;

            _stack = new Stack<GameObject>();
            _root = new GameObject(poolName).transform;
        }

        private GameObject InstantiatePrefab()
        {
            GameObject gameObject = GameObject.Instantiate(_prefab);
            gameObject.name = _prefabGameName;
            return gameObject;
        }

        public GameObject Pop()
        {
            GameObject gameObject;
            if (_stack.Count == 0)
            {
                gameObject = InstantiatePrefab();
            }
            else
            {
                gameObject = _stack.Pop();
            }

            gameObject.SetActive(true);
            gameObject.transform.SetParent(null);

            return gameObject;
        }

        public void Push(GameObject gameObject)
        {
            _stack.Push(gameObject);
            gameObject.transform.SetParent(_root);
            gameObject.SetActive(false);
        }

        public void Destroy()
        {
            while (_stack.Count > 0)
            {
                GameObject gameObject = _stack.Pop();
                GameObject.Destroy(gameObject);
            }
            GameObject.Destroy(_root.gameObject);
        }
    }
}
