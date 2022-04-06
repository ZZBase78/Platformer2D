using Platformer2D.Assets.Extention;
using Platformer2D.Assets.PlayerScripts;
using UnityEngine;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonController
    {
        private CannonData cannonData;
        private Player player;

        private CannonTargetAngleCalculator cannonTargetAngleCalculator;
        private CannonRotateController cannonRotateController;

        public CannonController(CannonData cannonData, Player player)
        {
            this.player = player;
            this.cannonData = cannonData;

            cannonTargetAngleCalculator = new CannonTargetAngleCalculator(cannonData, player);
            cannonRotateController = new CannonRotateController(cannonData);
        }
        public void Update(float deltaTime)
        {
            cannonTargetAngleCalculator.Calculate();
            cannonRotateController.Update(deltaTime);
        }
    }
}
