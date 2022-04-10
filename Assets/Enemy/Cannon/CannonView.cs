using Platformer2D.Assets.Interfaces;
using UnityEngine;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonView : MonoBehaviour, IDamageable
    {
        public Transform transformView;
        public Transform rotatePoint;
        public Transform fireStartPoint;
        public CannonController controller;

        public void SetDamage(float damage)
        {
            controller.SetDamage(damage);
        }
    }
}
