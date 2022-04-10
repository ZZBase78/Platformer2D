using Platformer2D.Assets.Pool;
using Platformer2D.Assets.Settings;
using System;
using UnityEngine;

namespace Platformer2D.Assets.PlayerBullet
{
    internal sealed class PlayerBulletFactory
    {
        private GameObject prefab;
        private PlayerBulletModel model;
        private PoolGameObject pool;

        public PlayerBulletFactory()
        {
            prefab = Resources.Load<GameObject>(ResourcesPathes.PLAYER_BULLET_PREFAB);
            model = Resources.Load<PlayerBulletModel>(ResourcesPathes.PLAYER_BULLET_MODEL);
            pool = new PoolGameObject(prefab, SceneObjectNames.PLAYER_BULLET, SceneObjectNames.PLAYER_BULLET_POOL);
        }

        public PlayerBulletData Create()
        {
            GameObject gameObject = pool.Pop();
            PlayerBulletView view = gameObject.GetComponent<PlayerBulletView>();
            if (view == null) throw new Exception(ErrorMessages.PLAYER_BULLET_VIEW_NOT_FOUND);

            PlayerBulletData playerBulletData = new PlayerBulletData();
            playerBulletData.damage = model.damage;
            playerBulletData.timeToDestroy = model.destroyTime;
            playerBulletData.speed = model.speed;


            playerBulletData.view = view;
            view.data = playerBulletData;
            view.controller = null;

            return playerBulletData;
        }

        public void Destroy(PlayerBulletData playerBulletData)
        {
            pool.Push(playerBulletData.view.gameObject);
            playerBulletData = null;
        }
    }
}
