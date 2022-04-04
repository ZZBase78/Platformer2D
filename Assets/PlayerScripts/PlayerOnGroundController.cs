using Platformer2D.Assets.CollidersScripts;
using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerOnGroundController
    {
        private const float pointOffSet = -0.52f;
        private const int maxCollidersCount = 8;

        private Player player;
        private Vector2 downOffSet;
        private Vector2 size;

        private CollidersController collidersController;

        public PlayerOnGroundController(Player player)
        {
            this.player = player;
            downOffSet = new Vector2(0, pointOffSet);
            size = new Vector2(1f, 0.05f);

            collidersController = new CollidersController(maxCollidersCount);
        }

        public void Update()
        {
            player.isOnGround = false;

            Vector2 point = (Vector2)player.view.transform.position + downOffSet;

            Collider2D wallCollider = collidersController.CheckTag(GameTags.WALL, point, size, 0);
            if (wallCollider)
            {
                player.isOnGround = true;
            }
        }
    }
}
