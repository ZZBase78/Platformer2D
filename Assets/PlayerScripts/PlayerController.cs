using Platformer2D.Assets.AnimationScripts;
using Platformer2D.Assets.Interfaces;
using Platformer2D.Assets.Settings;
using System;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerController
    {
        public event Action actionPlayerDie;

        private Player player;
        private AnimationController animationController;
        private PlayerStateAnimation playerStateAnimation;
        private PlayerScaleController playerScaleController;
        private PlayerJumpController playerJumpController;
        private PlayerOnGroundController playerOnGroundController;
        private PlayerFlyStateController playerFlyStateController;
        private PlayerPhysicsMoveController playerPhysicsMoveController;

        public PlayerController(Player player)
        {
            this.player = player;
            player.view.playerController = this;

            animationController = new AnimationController(player.view.spriteRenderer);
            playerStateAnimation = new PlayerStateAnimation();

            playerPhysicsMoveController = new PlayerPhysicsMoveController(player);

            playerScaleController = new PlayerScaleController(player);
            playerJumpController = new PlayerJumpController(player);
            playerOnGroundController = new PlayerOnGroundController(player);
            playerFlyStateController = new PlayerFlyStateController(player);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GameTags.COIN))
            {
                ICollectable collectable = collision.GetComponentInParent<ICollectable>();
                collectable.Collect();
            }
        }

        private void PlayerDie()
        {
            actionPlayerDie?.Invoke();
        }

        public void Update(float deltaTime)
        {
            playerOnGroundController.Update();

            playerPhysicsMoveController.Update(deltaTime);

            playerJumpController.Update();
            playerScaleController.Update();
            playerFlyStateController.Update();
            animationController.Play(playerStateAnimation.GetAnimationData(player.playerState));
            animationController.Update(deltaTime);
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            playerPhysicsMoveController.FixedUpdate(fixedDeltaTime);
        }
    }
}
