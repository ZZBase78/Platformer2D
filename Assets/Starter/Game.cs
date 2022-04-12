using Platformer2D.Assets.CameraScripts;
using Platformer2D.Assets.ChainMace;
using Platformer2D.Assets.Coin;
using Platformer2D.Assets.EndLevel;
using Platformer2D.Assets.Enemy.Cannon;
using Platformer2D.Assets.LevelScripts;
using Platformer2D.Assets.PlayerBullet;
using Platformer2D.Assets.PlayerScripts;
using Platformer2D.Assets.Portal;
using System.Collections.Generic;

namespace Platformer2D.Assets.Starter
{
    internal sealed class Game
    {
        private GameData gameData;

        private PlayerController playerController;
        private CameraController cameraController;
        private CannonManager cannonManager;
        private CoinManager coinManager;
        private PortalManager portalManager;
        private EndLevelController endLevelController;
        private PlayerBulletController playerBulletController;
        private ChainMaceController chainMaceController;
        
        public void Start()
        {
            gameData = new GameData();

            LevelData levelData = new LevelGenerator().Generate();
            new LevelDisplay().Display(levelData);

            List<CannonData> cannons = new CannonLevelPlacer(levelData).PlaceCannons();

            chainMaceController = new ChainMaceController(levelData);
            chainMaceController.PlaceRandom();

            PlayerFactory playerFactory = new PlayerFactory();
            Player player = playerFactory.GetPlayer();

            cannonManager = new CannonManager(cannons, gameData, player);

            coinManager = new CoinManager(levelData);

            new PlayerStartPosition().MoveToStart(player.view.transform, levelData);

            playerController = new PlayerController(gameData, player);

            cameraController = new CameraController();
            cameraController.SetTarget(player.view.transform);

            portalManager = new PortalManager(levelData);
            portalManager.CreateExitPortal();

            endLevelController = new EndLevelController(gameData);
            playerController.actionLevelExit += endLevelController.LevelExitPortalReached;
            playerController.actionPlayerDie += endLevelController.PlayerDie;

            playerBulletController = new PlayerBulletController();
            playerController.actionFire += playerBulletController.Fire;

            gameData.gameState = GameState.Playing;
        }

        public void Update(float deltaTime)
        {
            playerController.Update(deltaTime);
            cameraController.Update(deltaTime);
            cannonManager.Update(deltaTime);
            coinManager.Update(deltaTime);
            portalManager.Update(deltaTime);
            playerBulletController.Update(deltaTime);
            chainMaceController.Update(deltaTime);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            playerController.FixedUpdate(fixedDeltaTime);
        }
    }
}
