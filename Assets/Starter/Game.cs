using Platformer2D.Assets.CameraScripts;
using Platformer2D.Assets.ChainMace;
using Platformer2D.Assets.Chest;
using Platformer2D.Assets.Coin;
using Platformer2D.Assets.EndLevel;
using Platformer2D.Assets.Enemy.Bat;
using Platformer2D.Assets.Enemy.Cannon;
using Platformer2D.Assets.LevelScripts;
using Platformer2D.Assets.LevelScripts.Background;
using Platformer2D.Assets.PathFinder;
using Platformer2D.Assets.PlayerBullet;
using Platformer2D.Assets.PlayerScripts;
using Platformer2D.Assets.Portal;
using Platformer2D.Assets.World.Background;
using System.Collections.Generic;
using UnityEngine;

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
        private BatManager batManager;
        private PathFinderManager pathFinderManager;
        private ChestManager chestManager;
        private WorldBackgroundController worldBackgroundController;
        
        public void Start()
        {

            worldBackgroundController = new WorldBackgroundController();

            gameData = new GameData();

            LevelData levelData = new LevelGenerator().Generate();
            new LevelDisplay().Display(levelData);

            BackgroundController backgroundController = new BackgroundController(levelData);
            backgroundController.Generate();

            pathFinderManager = new PathFinderManager(levelData);

            List<CannonData> cannons = new CannonLevelPlacer(levelData).PlaceCannons();

            chainMaceController = new ChainMaceController(levelData);
            chainMaceController.PlaceRandom();

            PlayerFactory playerFactory = new PlayerFactory();
            Player player = playerFactory.GetPlayer();

            cannonManager = new CannonManager(cannons, gameData, player);

            coinManager = new CoinManager(levelData);

            batManager = new BatManager(levelData, pathFinderManager);
            batManager.PlaceRandom();

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

            chestManager = new ChestManager(levelData);
            chestManager.PlaceRandom();

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
            batManager.Update(deltaTime);
            chestManager.Update(deltaTime);

            worldBackgroundController.Update();

            pathFinderManager.Update();
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            playerController.FixedUpdate(fixedDeltaTime);
        }
    }
}
