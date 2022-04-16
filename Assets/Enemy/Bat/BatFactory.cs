using Platformer2D.Assets.Pool;
using Platformer2D.Assets.Settings;
using System;
using UnityEngine;

namespace Platformer2D.Assets.Enemy.Bat
{
    internal sealed class BatFactory
    {
        private GameObject prefab;
        private PoolGameObject pool;
        private BatModel model;

        public BatFactory()
        {
            prefab = Resources.Load<GameObject>(ResourcesPathes.BAT_PREFAB);
            pool = new PoolGameObject(prefab, SceneObjectNames.BAT, SceneObjectNames.BAT_POOL);
            model = Resources.Load<BatModel>(ResourcesPathes.BAT_MODEL);
        }

        public BatData Create()
        {
            GameObject gameObject = pool.Pop();
            BatView view = gameObject.GetComponent<BatView>();
            if (view == null) throw new Exception(ErrorMessages.CHAIN_MACE_VIEW_NOT_FOUND);

            BatData data = new BatData();
            data.view = view;
            data.normalSpeed = model.normalSpeed;
            data.chaseSpeed = model.chaseSpeed;
            data.state = BatState.Patrol;

            return data;
        }

        public void Destroy(BatData data)
        {
            pool.Push(data.view.gameObject);
            data = null;
        }
    }
}
