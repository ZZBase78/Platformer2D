using Platformer2D.Assets.Effects;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonDamageController
    {
        private CannonData cannonData;
        private EffectsController effectsController;

        public CannonDamageController(CannonData cannonData)
        {
            this.cannonData = cannonData;
            this.effectsController = new EffectsController();
        }

        public void SetDamage(float damage)
        {
            cannonData.health -= damage;
            if (cannonData.health <= 0) effectsController.Explode(cannonData.view.transformView.position);
        }
    }
}
