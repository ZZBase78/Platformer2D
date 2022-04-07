using Platformer2D.Assets.PlayerScripts;
using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonCheckPlayer
    {
        private const int MAX_COLLIDERS = 2;
        private CannonData cannonData;
        private Player player;
        private RaycastHit2D[] results;
        private int layerMask;

        public CannonCheckPlayer(CannonData cannonData, Player player)
        {
            this.cannonData = cannonData;
            this.player = player;
            results = new RaycastHit2D[MAX_COLLIDERS];
            layerMask = LayerMask.GetMask(LayerNames.PLAYER, LayerNames.WALL);
        }

        public void Update()
        {
            cannonData.isPlayerVisible = false;

            Vector3 startPosition = cannonData.view.fireStartPoint.position;
            Vector3 direction = player.view.transformView.position - startPosition;
            float distance = direction.magnitude;

            if (distance > cannonData.maxDistanceCheckPlayer) return;

            int collidersCount = Physics2D.RaycastNonAlloc(startPosition, direction, results, distance, layerMask);

            for (int i = 0; i < collidersCount; i++)
            {
                if (results[i].collider.CompareTag(GameTags.WALL)) return;
            }

            cannonData.isPlayerVisible = true;
        }
    }
}
