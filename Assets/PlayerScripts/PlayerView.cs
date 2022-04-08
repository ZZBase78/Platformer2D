using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerView : MonoBehaviour
    {
        public Transform transformView;
        public SpriteRenderer spriteRenderer;
        public Rigidbody2D rigidbodyView;
        public PlayerController playerController;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            playerController.OnTriggerEnter2D(collision);
        }
    }
}
