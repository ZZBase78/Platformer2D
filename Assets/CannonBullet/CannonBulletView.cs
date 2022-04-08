using UnityEngine;

namespace Platformer2D.Assets.CannonBullet
{
    internal sealed class CannonBulletView : MonoBehaviour
    {
        public Transform transformView;
        public Rigidbody2D rigidbodyView;
        public CannonBulletController cannonBulletController;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            cannonBulletController.OnCollisionEnter2D(collision);
        }
    }
}
