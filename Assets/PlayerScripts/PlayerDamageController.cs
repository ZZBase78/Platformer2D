using System;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerDamageController
    {
        private Player player;
        private Action actionPlayerDie;

        public PlayerDamageController(Player player, Action actionPlayerDie)
        {
            this.player = player;
            this.actionPlayerDie = actionPlayerDie;
        }

        public void SetDamage(float damage)
        {
            player.health -= damage;
            if (player.health <= 0) actionPlayerDie?.Invoke();
        }
    }
}
