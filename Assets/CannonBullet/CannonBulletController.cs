using Platformer2D.Assets.Interfaces;
using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.CannonBullet
{
    internal sealed class CannonBulletController
    {
        private CannonBulletData cannonBulletData;
        private CannonBulletExploder cannonBulletExploder;

        public CannonBulletController(CannonBulletData cannonBulletData)
        {
            this.cannonBulletData = cannonBulletData;
            cannonBulletData.view.cannonBulletController = this;
            cannonBulletExploder = new CannonBulletExploder(cannonBulletData);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            cannonBulletExploder.Explode();

            if (collision.collider.CompareTag(GameTags.PLAYER))
            {
                IDamageable damageable = collision.collider.GetComponentInParent<IDamageable>();
                if (damageable != null) damageable.SetDamage(cannonBulletData.damage);
            }
        }

        public void Update(float deltaTime)
        {
            cannonBulletData.timeToDestroy -= deltaTime;
            cannonBulletExploder.Update();
        }
    }
}
