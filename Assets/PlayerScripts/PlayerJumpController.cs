using Platformer2D.Assets.Starter;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerJumpController
    {
        private Player player;
        private GameData gameData;
        public PlayerJumpController(GameData gameData, Player player)
        {
            this.player = player;
            this.gameData = gameData;
        }

        public void Update()
        {
            if (gameData.gameState != GameState.Playing) return;
            if (Input.GetKeyDown(KeyCode.W) && player.isOnGround)
            {
                player.view.rigidbodyView.AddForce(Vector2.up * player.jumpForce);
            }
        }
    }
}
