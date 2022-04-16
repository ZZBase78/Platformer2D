using UnityEngine;

namespace Platformer2D.Assets.Enemy.Bat
{
    internal sealed class BatMoveController
    {
        private BatData batData;
        private BatTargetMove batTargetMove;

        public BatMoveController(BatData batData, BatTargetMove batTargetMove)
        {
            this.batData = batData;
            this.batTargetMove = batTargetMove;
        }

        public void Update(float detlaTime)
        {
            batData.view.transformView.position = Vector2.MoveTowards(batData.view.transformView.position, batTargetMove.target, batData.normalSpeed * detlaTime);
        }
    }
}
