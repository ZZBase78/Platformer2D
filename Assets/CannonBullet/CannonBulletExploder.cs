using Platformer2D.Assets.Effects;

namespace Platformer2D.Assets.CannonBullet
{
    internal sealed class CannonBulletExploder
    {
        private CannonBulletData cannonBulletData;
        private EffectsController effectsController;

        public CannonBulletExploder(CannonBulletData cannonBulletData)
        {
            this.cannonBulletData = cannonBulletData;
            effectsController = new EffectsController();
        }

        public void Explode()
        {
            effectsController.Explode(cannonBulletData.view.transformView.position);
            cannonBulletData.timeToDestroy = 0;
        }

        public void Update()
        {
            if (cannonBulletData.timeToDestroy <= 0)
            {
                Explode();
            }
        }
    }
}
