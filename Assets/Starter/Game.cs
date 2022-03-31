using Platformer2D.Assets.PlayerScripts;
using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.Starter
{
    internal sealed class Game
    {
        public void Start()
        {
            PlayerFactory playerFactory = new PlayerFactory();
            Player player = playerFactory.GetPlayer();

            GameObject playerPrefab = Resources.Load<GameObject>(ResourcesPathes.PLAYER);
            PlayerViewFactory playerViewFactory = new PlayerViewFactory(playerPrefab);
            PlayerView playerView = playerViewFactory.GetPlayerView();

            PlayerController playerController = new PlayerController(player, playerView);
        }

        public void Update(float deltaTime)
        {

        }
    }
}
