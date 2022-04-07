using Platformer2D.Assets.CannonBullet;
using Platformer2D.Assets.PlayerScripts;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonController
    {
        private CannonTargetAngleCalculator cannonTargetAngleCalculator;
        private CannonRotateController cannonRotateController;
        private CannonBulletManager cannonBulletManager;
        private CannonFireController cannonFireController;

        public CannonController(CannonData cannonData, Player player, CannonBulletManager cannonBulletManager)
        {
            cannonTargetAngleCalculator = new CannonTargetAngleCalculator(cannonData, player);
            cannonRotateController = new CannonRotateController(cannonData);
            cannonFireController = new CannonFireController(cannonData, cannonBulletManager);
            this.cannonBulletManager = cannonBulletManager;
        }
        public void Update(float deltaTime)
        {
            cannonTargetAngleCalculator.Calculate();
            cannonRotateController.Update(deltaTime);
            cannonFireController.Update(deltaTime);
        }
    }
}
