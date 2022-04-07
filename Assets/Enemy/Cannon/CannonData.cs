namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonData
    {
        public CannonView view;
        public float fireInterval;
        public float timeToFire;
        public float targetAngle;
        public float targetPossibleAngle;
        public float currentAngle;
        public float fireForce;
        public float rotateSpeed;
        public float minAngle;
        public float maxAngle;
        public float maxDistanceCheckPlayer;
        public bool isPlayerVisible;
    }
}
