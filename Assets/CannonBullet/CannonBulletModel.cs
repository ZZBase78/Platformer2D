using UnityEngine;

namespace Platformer2D.Assets.CannonBullet
{
    [CreateAssetMenu(fileName = "CannonBulletModel", menuName = "ScriptableObjects/Enemy/Cannon/CannonBulletModel", order = 1)]
    internal sealed class CannonBulletModel : ScriptableObject
    {
        public float damage;
        public float destroyTime;
    }
}
