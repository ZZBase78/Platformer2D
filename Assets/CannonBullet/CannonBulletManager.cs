using Platformer2D.Assets.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.CannonBullet
{
    internal class CannonBulletManager
    {
        private Dictionary<CannonBulletData, CannonBulletController> dictionary;
        private CannonBulletFactory cannonBulletFactory;
        private List<CannonBulletData> bulletToDestroy;

        public CannonBulletManager()
        {
            dictionary = new Dictionary<CannonBulletData, CannonBulletController>();
            cannonBulletFactory = new CannonBulletFactory();
            bulletToDestroy = new List<CannonBulletData>();
        }

        public void Update(float deltaTime)
        {
            foreach (var keyValuePair in dictionary)
            {
                keyValuePair.Value.Update(deltaTime);
                if (keyValuePair.Key.timeToDestroy <= 0) bulletToDestroy.Add(keyValuePair.Key);
            }
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            for(int i = 0; i < bulletToDestroy.Count; i++)
            {
                RemoveBullet(bulletToDestroy[i]);
                cannonBulletFactory.Destroy(bulletToDestroy[i]);
            }
            bulletToDestroy.Clear();
        }

        private void AddBullet(CannonBulletData cannonBulletData)
        {
            dictionary.Add(cannonBulletData, new CannonBulletController(cannonBulletData));
        }

        private void RemoveBullet(CannonBulletData cannonBulletData)
        {
            dictionary.Remove(cannonBulletData);
        }

        public void Fire(Vector2 position, Vector2 direction, float force)
        {
            CannonBulletData cannonBulletData = cannonBulletFactory.GetBulletData();
            AddBullet(cannonBulletData);
            cannonBulletData.view.transformView.position = position;
            cannonBulletData.view.transformView.up = direction;
            cannonBulletData.view.rigidbodyView.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }
}
