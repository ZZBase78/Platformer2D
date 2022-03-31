namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerController
    {
        private Player player;
        private PlayerView playerView;

        public PlayerController(Player player, PlayerView playerView)
        {
            this.player = player;
            this.playerView = playerView;
        }
    }
}
