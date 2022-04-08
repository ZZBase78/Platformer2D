using UnityEngine;

namespace Platformer2D.Assets.CannonBullet
{
    internal sealed class CannonBulletExploder
    {
        private CannonBulletData cannonBulletData;
        private GameObject explosionPrefab;

        public CannonBulletExploder(CannonBulletData cannonBulletData, GameObject explosionPrefab)
        {
            this.cannonBulletData = cannonBulletData;
            this.explosionPrefab = explosionPrefab;
        }

        public void Explode()
        {
            GameObject gameObject = GameObject.Instantiate(explosionPrefab);
            gameObject.transform.position = cannonBulletData.view.transformView.position;
            cannonBulletData.timeToDestroy = 0;
        }

        public void Update()
        {
            if (cannonBulletData.timeToDestroy <= 0)
            {
                Explode();
            }
        }
    }
}
