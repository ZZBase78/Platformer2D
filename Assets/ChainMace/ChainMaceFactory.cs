using Platformer2D.Assets.Pool;
using Platformer2D.Assets.Settings;
using System;
using UnityEngine;

namespace Platformer2D.Assets.ChainMace
{
    internal sealed class ChainMaceFactory
    {
        private GameObject prefab;
        private PoolGameObject pool;

        public ChainMaceFactory()
        {
            prefab = Resources.Load<GameObject>(ResourcesPathes.CHAIN_MACE_PREFAB);
            pool = new PoolGameObject(prefab, SceneObjectNames.CHAIN_MACE, SceneObjectNames.CHAIN_MACE_POOL);
        }

        public ChainMaceData Create()
        {
            GameObject gameObject = pool.Pop();
            ChainMaceView view = gameObject.GetComponent<ChainMaceView>();
            if (view == null) throw new Exception(ErrorMessages.CHAIN_MACE_VIEW_NOT_FOUND);
            view.transformView = view.transform;

            ChainMaceData data = new ChainMaceData();
            data.view = view;

            view.data = data;


            return data;
        }

        public void Destroy(ChainMaceData data)
        {
            pool.Push(data.view.gameObject);
            data = null;
        }
    }
}
