using Platformer2D.Assets.CameraScripts;
using Platformer2D.Assets.Enemy.Cannon;
using Platformer2D.Assets.LevelScripts;
using Platformer2D.Assets.PlayerScripts;
using System.Collections.Generic;

namespace Platformer2D.Assets.Starter
{
    internal sealed class Game
    {
        private PlayerController playerController;
        private CameraController cameraController;
        private CannonManager cannonManager;

        public void Start()
        {

            LevelData levelData = new LevelGenerator().Generate();
            new LevelDisplay().Display(levelData);

            List<CannonData> cannons = new CannonLevelPlacer(levelData).PlaceCannons();


            PlayerFactory playerFactory = new PlayerFactory();
            Player player = playerFactory.GetPlayer();

            cannonManager = new CannonManager(cannons, player);

            new PlayerStartPosition().MoveToStart(player.view.transform, levelData);

            playerController = new PlayerController(player);

            cameraController = new CameraController();
            cameraController.SetTarget(player.view.transform);
            playerController.actionPlayerDie += cameraController.UnSetTarget;
        }

        public void Update(float deltaTime)
        {
            playerController.Update(deltaTime);
            cameraController.Update(deltaTime);
            cannonManager.Update(deltaTime);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            playerController.FixedUpdate(fixedDeltaTime);
        }
    }
}
