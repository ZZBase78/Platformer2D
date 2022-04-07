namespace Platformer2D.Assets.CannonBullet
{
    internal sealed class CannonBulletController
    {
        private CannonBulletData cannonBulletData;

        public CannonBulletController(CannonBulletData cannonBulletData)
        {
            this.cannonBulletData = cannonBulletData;
        }

        public void Update(float deltaTime)
        {
            cannonBulletData.timeToDestroy -= deltaTime;
        }
    }
}
