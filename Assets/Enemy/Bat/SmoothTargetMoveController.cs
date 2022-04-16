using UnityEngine;

namespace Platformer2D.Assets.Enemy.Bat
{
    internal sealed class SmoothTargetMoveController
    {
        private BatTargetMove batTargetMove;
        private BatData batData;

        public SmoothTargetMoveController(BatData batData, BatTargetMove batTargetMove)
        {
            this.batData = batData;
            this.batTargetMove = batTargetMove;
        }

        public void Update(float deltaTime)
        {
            batTargetMove.smoothTarget = Vector2.MoveTowards(batTargetMove.smoothTarget, batTargetMove.target, batData.normalSpeed * deltaTime);
        }
    }
}
