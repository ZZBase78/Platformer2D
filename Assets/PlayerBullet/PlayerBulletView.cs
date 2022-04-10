using UnityEngine;

namespace Platformer2D.Assets.PlayerBullet
{
    internal sealed class PlayerBulletView : MonoBehaviour
    {
        public Transform transformView;
        public PlayerBulletData data;
        public PlayerBulletController controller;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            controller.OnTriggerEnter2D(data, collision);
        }
    }
}
