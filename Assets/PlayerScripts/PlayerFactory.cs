using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerFactory
    {

        private PlayerViewFactory playerViewFactory;

        public PlayerFactory()
        {
            playerViewFactory = new PlayerViewFactory(Resources.Load<GameObject>(ResourcesPathes.PLAYER));
        }

        public Player GetPlayer()
        {
            Player player = new Player();
            player.view = playerViewFactory.GetPlayerView();
            return player;
        }

    }
}
