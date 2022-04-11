using Platformer2D.Assets.Interfaces;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerView : MonoBehaviour, IDamageable
    {
        public Transform transformView;
        public SpriteRenderer spriteRenderer;
        public Rigidbody2D rigidbodyView;
        public PlayerController playerController;

        public void SetDamage(float damage)
        {
            playerController.SetDamage(damage);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            playerController.OnTriggerEnter2D(collision);
        }
    }
}
