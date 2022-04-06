using UnityEngine;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonRotateController
    {
        private CannonData cannonData;

        public CannonRotateController(CannonData cannonData)
        {
            this.cannonData = cannonData;
        }

        public void Update(float deltaTime)
        {

            Debug.Log(cannonData.targetAngle);

            cannonData.currentAngle = Mathf.MoveTowards(cannonData.currentAngle, cannonData.targetPossibleAngle, cannonData.rotateSpeed * deltaTime);

            Vector3 rotatedVector = Quaternion.Euler(0, 0, -cannonData.currentAngle) * cannonData.view.transformView.up;

            cannonData.view.rotatePoint.up = rotatedVector;
        }
    }
}
