using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerMoveController
    {
        private Player player;

        public PlayerMoveController(Player player)
        {
            this.player = player;
        }

        public void Move(float deltaTime)
        {
            Vector3 direction = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
                direction = Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction = Vector3.right;
            }

            player.view.transform.Translate(direction * deltaTime * player.moveSpeed);
        }
    }
}
