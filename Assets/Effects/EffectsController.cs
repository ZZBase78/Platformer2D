using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.Effects
{
    internal sealed class EffectsController
    {
        private GameObject _explosionPrefab;
        private GameObject explosionPrefab
        { 
            get
            { 
                if (_explosionPrefab == null)
                {
                    _explosionPrefab = Resources.Load<GameObject>(ResourcesPathes.EXPLOSION_PREFAB);
                }
                return _explosionPrefab;
            }
        }

        public void Explode(Vector2 position)
        {
            GameObject gameObject = GameObject.Instantiate(explosionPrefab);
            gameObject.transform.position = position;
        }
    }
}
