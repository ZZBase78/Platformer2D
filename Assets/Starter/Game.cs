using Platformer2D.Assets.LevelScripts;
using Platformer2D.Assets.PlayerScripts;

namespace Platformer2D.Assets.Starter
{
    internal sealed class Game
    {
        private PlayerController playerController;

        public void Start()
        {
            //PlayerFactory playerFactory = new PlayerFactory();
            //Player player = playerFactory.GetPlayer();

            //playerController = new PlayerController(player);

            LevelData levelData = new LevelGenerator().Generate();
            new LevelDisplay().Display(levelData);
        }

        public void Update(float deltaTime)
        {
            //playerController.Update(deltaTime);
        }
    }
}
