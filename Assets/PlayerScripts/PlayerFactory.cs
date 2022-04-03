using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerFactory
    {

        private PlayerViewFactory playerViewFactory;
        private PlayerModel playerModel;

        public PlayerFactory()
        {
            playerViewFactory = new PlayerViewFactory(Resources.Load<GameObject>(ResourcesPathes.PLAYER));
            playerModel = Resources.Load<PlayerModel>(ResourcesPathes.PLAYER_MODEL);
        }

        public Player GetPlayer()
        {
            Player player = new Player();
            player.moveSpeed = playerModel.moveSpeed;
            player.jumpForce = playerModel.jumpForce;

            player.view = playerViewFactory.GetPlayerView();
            player.playerState = PlayerState.Stand;
            return player;
        }

    }
}
