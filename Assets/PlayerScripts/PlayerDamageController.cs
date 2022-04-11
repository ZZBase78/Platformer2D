namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerDamageController
    {
        private Player player;

        public PlayerDamageController(Player player)
        {
            this.player = player;
        }

        public void SetDamage(float damage)
        {
            player.health -= damage;
        }
    }
}
