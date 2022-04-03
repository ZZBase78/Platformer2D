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
            PlayerState newPlayerState = PlayerState.Stand;
            if (Input.GetKey(KeyCode.A))
            {
                direction = Vector3.left;
                newPlayerState = PlayerState.MoveLeft;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction = Vector3.right;
                newPlayerState = PlayerState.MoveRight;
            }

            player.view.transform.Translate(direction * deltaTime * player.moveSpeed);
            player.playerState = newPlayerState;
        }
    }
}
