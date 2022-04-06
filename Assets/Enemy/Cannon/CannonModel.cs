using UnityEngine;

namespace Platformer2D.Assets.Enemy.Cannon
{
    [CreateAssetMenu(fileName = "CannonModel", menuName = "ScriptableObjects/Enemy/Cannon/CannonModel", order = 1)]
    internal sealed class CannonModel : ScriptableObject
    {
        public float fireInterval;
        public float fireForce;
        public float rotateSpeed;
        public float minAngle;
        public float maxAngle;
    }
}
