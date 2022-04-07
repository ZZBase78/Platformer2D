using Platformer2D.Assets.Settings;
using System;
using UnityEngine;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonFactory
    {
        private GameObject cannonPrefab;
        private CannonModel cannonModel;
        public CannonFactory()
        {
            cannonPrefab = Resources.Load<GameObject>(ResourcesPathes.CANNON_PREFAB);
            cannonModel = Resources.Load<CannonModel>(ResourcesPathes.CANNON_MODEL);
        }

        public CannonData GetCannon()
        {
            GameObject go = GameObject.Instantiate(cannonPrefab);
            CannonData cannonData = new CannonData();
            cannonData.view = go.GetComponent<CannonView>();

            if (cannonData.view == null) throw new Exception(ErrorMessages.CANNON_VIEW_NOT_FOUND);

            cannonData.fireInterval = cannonModel.fireInterval;
            cannonData.timeToFire = 0f;
            cannonData.targetAngle = 0f;
            cannonData.targetPossibleAngle = 0f;
            cannonData.currentAngle = 0f;
            cannonData.fireForce = cannonModel.fireForce;
            cannonData.rotateSpeed = cannonModel.rotateSpeed;
            cannonData.minAngle = cannonModel.minAngle;
            cannonData.maxAngle = cannonModel.maxAngle;

            return cannonData;
    }
    }
}
