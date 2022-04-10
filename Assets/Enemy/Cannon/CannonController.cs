using Platformer2D.Assets.CannonBullet;
using Platformer2D.Assets.PlayerScripts;
using Platformer2D.Assets.Starter;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonController
    {
        private CannonTargetAngleCalculator cannonTargetAngleCalculator;
        private CannonRotateController cannonRotateController;
        private CannonFireController cannonFireController;
        private CannonCheckPlayer cannonCheckPlayer;
        private GameData gameData;
        private CannonData cannonData;
        private CannonDamageController cannonDamageController;

        public CannonController(GameData gameData, CannonData cannonData, Player player, CannonBulletManager cannonBulletManager)
        {
            this.gameData = gameData;
            this.cannonData = cannonData;
            cannonData.view.controller = this;

            cannonTargetAngleCalculator = new CannonTargetAngleCalculator(cannonData, player);
            cannonRotateController = new CannonRotateController(cannonData);
            cannonFireController = new CannonFireController(cannonData, cannonBulletManager);
            cannonCheckPlayer = new CannonCheckPlayer(cannonData, player);
            cannonDamageController = new CannonDamageController(cannonData);
        }

        public void SetDamage(float damage)
        {
            cannonDamageController.SetDamage(damage);
        }

        public void Update(float deltaTime)
        {
            cannonCheckPlayer.Update();
            cannonTargetAngleCalculator.Calculate();
            cannonRotateController.Update(deltaTime);
            
            if (gameData.gameState == GameState.Playing) cannonFireController.Update(deltaTime);
        }
    }
}
