using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerJumpController
    {
        private Player player;
        public PlayerJumpController(Player player)
        {
            this.player = player;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                player.view.rigidbodyView.AddForce(Vector2.up * player.jumpForce);
            }
        }
    }
}
