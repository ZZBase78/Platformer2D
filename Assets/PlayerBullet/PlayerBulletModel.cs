using UnityEngine;

namespace Platformer2D.Assets.PlayerBullet
{
    [CreateAssetMenu(fileName = "PlayerBulletModel", menuName = "ScriptableObjects/Player/PlayerBulletModel", order = 1)]
    internal sealed class PlayerBulletModel : ScriptableObject
    {
        public float damage;
        public float destroyTime;
        public float speed;
    }
}
