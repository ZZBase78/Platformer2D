using UnityEngine;

namespace Platformer2D.Assets.Enemy.Bat
{
    internal sealed class BatMoveController
    {
        private const float SMOOTH_PARAM = 2f;

        private BatData batData;
        private BatTargetMove batTargetMove;

        public BatMoveController(BatData batData, BatTargetMove batTargetMove)
        {
            this.batData = batData;
            this.batTargetMove = batTargetMove;
        }

        private void SetMoveDirection(Vector3 currentPosition, Vector3 nextPosition)
        {
            if (nextPosition.x > currentPosition.x)
            {
                batData.horizontalMoveDirection = HorizontalMoveDirection.Right;
            }
            else if (nextPosition.x < currentPosition.x)
            {
                batData.horizontalMoveDirection = HorizontalMoveDirection.Left;
            }
            else
            {
                batData.horizontalMoveDirection = HorizontalMoveDirection.None;
            }
        }

        public void Update(float deltaTime)
        {
            Vector3 currentPosition = batData.view.transformView.position;
            Vector3 nextPosition = Vector2.Lerp(currentPosition, batTargetMove.smoothTarget, SMOOTH_PARAM * deltaTime);
            SetMoveDirection(currentPosition, nextPosition);

            batData.view.transformView.position = nextPosition;
        }
    }
}
