using UnityEngine;

namespace Platformer2D.Assets.Enemy.Bat
{
    internal sealed class BatSpriteFlipController
    {
        private BatData batData;

        public BatSpriteFlipController(BatData batData)
        {
            this.batData = batData;
        }

        public void Update()
        {
            if (batData.horizontalMoveDirection == HorizontalMoveDirection.Left)
            {
                batData.view.spriteRenderer.flipX = false;
            }
            else if (batData.horizontalMoveDirection == HorizontalMoveDirection.Right)
            {
                batData.view.spriteRenderer.flipX = true;
            }
        }
    }
}
