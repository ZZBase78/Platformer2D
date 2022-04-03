using Platformer2D.Assets.AnimationScripts;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerController
    {
        private Player player;
        private AnimationController playerAnimation;
        private PlayerMoveController playerMoveController;

        public PlayerController(Player player)
        {
            this.player = player;
            playerAnimation = new AnimationController(player.view.spriteRenderer);
            playerAnimation.Play(new AnimationFactory().Create(ResourcesAnimationPathes.PLAYER_IDLE));

            playerMoveController = new PlayerMoveController(player);
        }

        public void Update(float deltaTime)
        {
            playerMoveController.Move(deltaTime);
            playerAnimation.Update(deltaTime);

        }
    }
}
