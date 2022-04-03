using Platformer2D.Assets.LevelScripts;
using Platformer2D.Assets.PlayerScripts;

namespace Platformer2D.Assets.Starter
{
    internal sealed class Game
    {
        private PlayerController playerController;

        public void Start()
        {

            LevelData levelData = new LevelGenerator().Generate();
            new LevelDisplay().Display(levelData);

            PlayerFactory playerFactory = new PlayerFactory();
            Player player = playerFactory.GetPlayer();

            new PlayerStartPosition().MoveToStart(player.view.transform, levelData);

            playerController = new PlayerController(player);
        }

        public void Update(float deltaTime)
        {
            playerController.Update(deltaTime);
        }
    }
}
