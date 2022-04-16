using UnityEngine;

namespace Platformer2D.Assets.Enemy.Bat
{
    internal sealed class BatTargetMove
    {
        public Vector3 target;
        public Vector3 smoothTarget;

        public BatTargetMove(Vector3 target)
        {
            this.target = target;
            this.smoothTarget = target;
        }
    }
}
