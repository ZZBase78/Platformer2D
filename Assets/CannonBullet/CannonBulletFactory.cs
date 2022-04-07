using Platformer2D.Assets.Pool;
using Platformer2D.Assets.Settings;
using System;
using UnityEngine;

namespace Platformer2D.Assets.CannonBullet
{
    internal sealed class CannonBulletFactory
    {
        private PoolGameObject pool;
        private CannonBulletModel model;
        public CannonBulletFactory()
        {
            GameObject cannonBulletPrefab = Resources.Load<GameObject>(ResourcesPathes.CANNON_BULLET_PREFAB);
            pool = new PoolGameObject(cannonBulletPrefab, SceneObjectNames.CANNON_BULLET, SceneObjectNames.CANNON_BULLET_POOL);
            model = Resources.Load<CannonBulletModel>(ResourcesPathes.CANNON_BULLET_MODEL);
        }

        public CannonBulletData GetBulletData()
        {
            CannonBulletData cannonBulletData = new CannonBulletData();

            GameObject gameObject = pool.Pop();
            CannonBulletView view = gameObject.GetComponent<CannonBulletView>();
            if (view == null) throw new Exception(ErrorMessages.CANNON_BULLET_VIEW_NOT_FOUND);

            cannonBulletData.view = view;

            cannonBulletData.damage = model.damage;
            cannonBulletData.timeToDestroy = model.destroyTime;

            return cannonBulletData;
        }

        public void Destroy(CannonBulletData cannonBulletData)
        {
            pool.Push(cannonBulletData.view.gameObject);
        }
    }
}
