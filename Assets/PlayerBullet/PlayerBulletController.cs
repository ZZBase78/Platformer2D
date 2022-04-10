using Platformer2D.Assets.Effects;
using Platformer2D.Assets.Interfaces;
using Platformer2D.Assets.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.PlayerBullet
{
    internal sealed class PlayerBulletController
    {
        private List<PlayerBulletData> bullets;
        private List<PlayerBulletData> addBullets;
        private List<PlayerBulletData> removeBullets;
        private PlayerBulletFactory factory;

        private PlayerBulletMoveController moveController;
        private EffectsController effectsController;

        public PlayerBulletController()
        {
            bullets = new List<PlayerBulletData>();
            addBullets = new List<PlayerBulletData>();
            removeBullets = new List<PlayerBulletData>();
            factory = new PlayerBulletFactory();
            moveController = new PlayerBulletMoveController();
            effectsController = new EffectsController();
        }

        public void OnTriggerEnter2D(PlayerBulletData playerBulletData, Collider2D collision)
        {
            if (collision.CompareTag(GameTags.ENEMY))
            {
                IDamageable damageable = collision.GetComponentInParent<IDamageable>();
                if (damageable != null)
                {
                    damageable.SetDamage(playerBulletData.damage);
                    effectsController.Explode(playerBulletData.view.transformView.position);
                    playerBulletData.timeToDestroy = 0;
                }
            }
            else if (collision.CompareTag(GameTags.WALL))
            {
                effectsController.Explode(playerBulletData.view.transformView.position);
                playerBulletData.timeToDestroy = 0;
            }
        }

        public void Update(float deltaTime)
        {
            AddBullets();
            RemoveBullets();
            foreach (PlayerBulletData playerBulletData in bullets)
            {
                UpdateBullet(playerBulletData, deltaTime);
            }
        }

        public void Fire(Vector2 position, Vector2 direction)
        {
            PlayerBulletData bullet = factory.Create();
            bullet.view.transformView.position = position;
            bullet.view.transformView.up = direction;
            bullet.view.controller = this;

            addBullets.Add(bullet);
        }

        private void AddBullets()
        {
            foreach (PlayerBulletData playerBulletData in addBullets)
            {
                bullets.Add(playerBulletData);
            }
            addBullets.Clear();
        }

        private void RemoveBullets()
        {
            foreach (PlayerBulletData playerBulletData in removeBullets)
            {
                bullets.Remove(playerBulletData);
                factory.Destroy(playerBulletData);
            }
            removeBullets.Clear();
        }

        private void UpdateBullet(PlayerBulletData playerBulletData, float deltaTime)
        {
            playerBulletData.timeToDestroy -= deltaTime;

            moveController.Move(playerBulletData, deltaTime);
            
            if (playerBulletData.timeToDestroy <= 0) removeBullets.Add(playerBulletData);
        }
    }
}
