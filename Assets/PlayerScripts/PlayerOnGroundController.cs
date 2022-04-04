using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerOnGroundController
    {
        private const float pointOffSet = -0.52f;
        private const int maxCollidersCount = 8;

        private Player player;
        private Collider2D[] results;
        private Vector2 downOffSet;
        private Vector2 size;

        public PlayerOnGroundController(Player player)
        {
            this.player = player;
            results = new Collider2D[maxCollidersCount];
            downOffSet = new Vector2(0, pointOffSet);
            size = new Vector2(1f, 0.05f);
        }

        public void Update()
        {
            player.isOnGround = false;

            Vector2 point = (Vector2)player.view.transform.position + downOffSet;
            int resultCount = Physics2D.OverlapBoxNonAlloc(point, size, 0, results);
            if (resultCount == 0) return;

            for(int i = 0; i < resultCount; i++)
            {
                if (results[i].CompareTag(GameTags.WALL))
                {
                    player.isOnGround = true;
                }
            }
        }
    }
}
