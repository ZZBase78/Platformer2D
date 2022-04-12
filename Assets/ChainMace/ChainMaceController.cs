using Platformer2D.Assets.BaseClasses;
using Platformer2D.Assets.LevelScripts;
using Platformer2D.Assets.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.ChainMace
{
    internal sealed class ChainMaceController : BaseController<ChainMaceData>
    {
        private LevelData levelData;
        private ChainMaceLevelPlacer chainMaceLevelPlacer;
        private ChainMaceFactory factory;
        private LevelCoordinator levelCoordinator;
        private ChainMaceMotorPingPongModel motorModel;

        public ChainMaceController(LevelData _levelData)
        {
            levelData = _levelData;

            chainMaceLevelPlacer = new ChainMaceLevelPlacer(levelData);
            factory = new ChainMaceFactory();
            levelCoordinator = new LevelCoordinator(levelData);
            motorModel = Resources.Load<ChainMaceMotorPingPongModel>(ResourcesPathes.CHAIN_MACE_MOTOR_PING_PONG_MODEL);
        }

        private ChainMaceData Create()
        {
            ChainMaceData chainMaceData = factory.Create();
            chainMaceData.view.controller = this;
            chainMaceData.jointMotor2DPingPongController = new JointMotor2DPingPongController(chainMaceData.view.hingeJoint2D, motorModel.motorSpeed, motorModel.timeToChangeDirection, motorModel.randomStartTime);
            return chainMaceData;
        }

        public void PlaceRandom()
        {
            List<Vector2Int> placedPositions = chainMaceLevelPlacer.Place();

            foreach(Vector2Int position in placedPositions)
            {
                ChainMaceData chainMaceData = Create();
                chainMaceData.view.transformView.position = position + levelCoordinator.worldOffSet;
                AddElement(chainMaceData);
            }
        }

        protected override void Destroy(ChainMaceData value)
        {
            factory.Destroy(value);
        }

        protected override void UpdateElement(ChainMaceData value, float deltaTime)
        {
            value.jointMotor2DPingPongController.Update(deltaTime);
        }
    }
}
