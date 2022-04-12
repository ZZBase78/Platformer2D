using UnityEngine;

namespace Platformer2D.Assets.ChainMace
{
    [CreateAssetMenu(fileName = "ChainMaceMotorPingPongModel", menuName = "ScriptableObjects/ChainMace/ChainMaceMotorPingPongModel", order = 1)]
    internal sealed class ChainMaceMotorPingPongModel : ScriptableObject
    {
        public float motorSpeed;
        public float timeToChangeDirection;
        public float randomStartTime;
    }
}
