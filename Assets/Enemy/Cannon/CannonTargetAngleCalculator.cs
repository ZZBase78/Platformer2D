using Platformer2D.Assets.Extention;
using Platformer2D.Assets.PlayerScripts;
using UnityEngine;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonTargetAngleCalculator
    {
        private Transform playerTransform;
        private Transform rotatePoint;
        private CannonData cannonData;
        private Transform cannonTransform;

        public CannonTargetAngleCalculator(CannonData cannonData, Player player)
        {
            this.cannonData = cannonData;
            cannonTransform = cannonData.view.transformView;
            rotatePoint = cannonData.view.rotatePoint;
            
            playerTransform = player.view.transformView;
        }
        public void Calculate()
        {
            Vector2 direction = playerTransform.position - rotatePoint.position;
            cannonData.targetAngle = Vector3Extension.SignAngle(cannonTransform.up, direction, Vector3.forward);

            cannonData.targetPossibleAngle = Mathf.Clamp(cannonData.targetAngle, cannonData.minAngle, cannonData.maxAngle);
        }
    }
}
