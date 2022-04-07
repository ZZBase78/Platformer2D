using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerFlyStateController
    {
        private const float minABSVelocity = 2f;

        private Player player;

        public PlayerFlyStateController(Player player)
        {
            this.player = player;
        }

        public void Update()
        {
            float yVelocity = player.view.rigidbodyView.velocity.y;

            if (Mathf.Abs(yVelocity) < minABSVelocity)
            {
                if (player.isOnGround) return;
                player.playerState = PlayerState.Stand;
                return;
            }

            if (yVelocity >= 0)
            {
                player.playerState = PlayerState.JumpUp;
            }
            else
            {
                player.playerState = PlayerState.FallDown;
            }
        }
    }
}
