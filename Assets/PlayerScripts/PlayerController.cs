using Platformer2D.Assets.AnimationScripts;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerController
    {
        private Player player;
        private AnimationController animationController;
        private PlayerStateAnimation playerStateAnimation;
        private PlayerMoveController playerMoveController;
        private PlayerScaleController playerScaleController;

        public PlayerController(Player player)
        {
            this.player = player;
            animationController = new AnimationController(player.view.spriteRenderer);
            playerStateAnimation = new PlayerStateAnimation();

            playerMoveController = new PlayerMoveController(player);

            playerScaleController = new PlayerScaleController(player);
        }

        public void Update(float deltaTime)
        {
            playerMoveController.Move(deltaTime);

            playerScaleController.Update();

            animationController.Play(playerStateAnimation.GetAnimationData(player.playerState));
            animationController.Update(deltaTime);
        }
    }
}
