using UnityEngine;

namespace Platformer2D.Assets.CannonBullet
{
    internal sealed class CannonBulletController
    {
        private CannonBulletData cannonBulletData;
        private CannonBulletExploder cannonBulletExploder;

        public CannonBulletController(CannonBulletData cannonBulletData, GameObject explosionPrefab)
        {
            this.cannonBulletData = cannonBulletData;
            cannonBulletData.view.cannonBulletController = this;
            cannonBulletExploder = new CannonBulletExploder(cannonBulletData, explosionPrefab);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            cannonBulletExploder.Explode();
        }

        public void Update(float deltaTime)
        {
            cannonBulletData.timeToDestroy -= deltaTime;
            cannonBulletExploder.Update();
        }
    }
}
