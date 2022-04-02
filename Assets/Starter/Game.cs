using Platformer2D.Assets.LevelScripts;
using Platformer2D.Assets.PlayerScripts;
using Platformer2D.Assets.Settings;
using UnityEngine;

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

            LevelConfigSettings levelConfigSettings = Resources.Load<LevelConfigSettings>(ResourcesPathes.LEVEL_CONFIG_SETTINGS);
            LevelConfig levelConfig = new LevelConfigGenerator().Generate(levelConfigSettings.width, levelConfigSettings.height);
        }

        public void Update(float deltaTime)
        {
            //playerController.Update(deltaTime);
        }
    }
}
