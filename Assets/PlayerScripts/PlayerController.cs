using Platformer2D.Assets.AnimationScripts;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerController
    {
        private Player player;
        private AnimationController playerAnimation;

        public PlayerController(Player player)
        {
            this.player = player;
            playerAnimation = new AnimationController(player.view.spriteRenderer);
            playerAnimation.Play(new AnimationFactory().Create(ResourcesAnimationPathes.PLAYER_IDLE));
        }

        public void Update(float deltaTime)
        {
            playerAnimation.Update(deltaTime);
        }
    }
}
