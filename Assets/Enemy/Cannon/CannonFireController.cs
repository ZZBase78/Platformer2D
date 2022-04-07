using Platformer2D.Assets.CannonBullet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonFireController
    {
        private const float MIN_ANGLE_TO_FIRE = 1f;

        private CannonData cannonData;
        private CannonBulletManager cannonBulletManager;

        public CannonFireController(CannonData cannonData, CannonBulletManager cannonBulletManager)
        {
            this.cannonData = cannonData;
            this.cannonBulletManager = cannonBulletManager;
        }

        private void Fire()
        {
            cannonData.timeToFire = cannonData.fireInterval;
            cannonBulletManager.Fire(cannonData.view.fireStartPoint.position, cannonData.view.fireStartPoint.up, cannonData.fireForce);
        }

        public void Update(float deltaTime)
        {
            if (cannonData.timeToFire > 0) cannonData.timeToFire -= deltaTime;
                
            if (cannonData.timeToFire > 0) return;

            if (!cannonData.isPlayerVisible) return;

            if (Mathf.Abs(cannonData.targetAngle - cannonData.currentAngle) > MIN_ANGLE_TO_FIRE) return;

            Fire();
        }
    }
}
