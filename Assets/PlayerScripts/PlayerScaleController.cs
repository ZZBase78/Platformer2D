using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerScaleController
    {
        private Player player;
        private Vector3 normalScale;
        private Vector3 inversionXScale;
        public PlayerScaleController(Player player)
        {
            this.player = player;
            normalScale = Vector3.one;
            inversionXScale = new Vector3(-1, 1, 1);
        }

        public void Update()
        {
            if (player.playerState == PlayerState.MoveRight)
            {
                player.view.transform.localScale = normalScale;
            }
            else if (player.playerState == PlayerState.MoveLeft)
            {
                player.view.transform.localScale = inversionXScale;
            }
        }
    }
}
