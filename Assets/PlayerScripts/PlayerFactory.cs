namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerFactory
    {
        public Player GetPlayer()
        {
            Player player = new Player();
            return player;
        }
    }
}
