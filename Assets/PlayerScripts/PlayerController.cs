using Platformer2D.Assets.AnimationScripts;
using Platformer2D.Assets.Interfaces;
using Platformer2D.Assets.Settings;
using Platformer2D.Assets.Starter;
using System;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerController
    {
        public event Action actionPlayerDie;
        public event Action actionLevelExit;

        private Player player;
        private GameData gameData;
        private AnimationController animationController;
        private PlayerStateAnimation playerStateAnimation;
        private PlayerScaleController playerScaleController;
        private PlayerJumpController playerJumpController;
        private PlayerOnGroundController playerOnGroundController;
        private PlayerFlyStateController playerFlyStateController;
        private PlayerPhysicsMoveController playerPhysicsMoveController;

        public PlayerController(GameData gameData, Player player)
        {
            this.player = player;
            this.gameData = gameData;
            player.view.playerController = this;

            animationController = new AnimationController(player.view.spriteRenderer);
            playerStateAnimation = new PlayerStateAnimation();

            playerPhysicsMoveController = new PlayerPhysicsMoveController(gameData, player);

            playerScaleController = new PlayerScaleController(player);
            playerJumpController = new PlayerJumpController(gameData, player);
            playerOnGroundController = new PlayerOnGroundController(player);
            playerFlyStateController = new PlayerFlyStateController(player);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GameTags.COIN))
            {
                ICollectable collectable = collision.GetComponentInParent<ICollectable>();
                if (collectable != null) collectable.Collect();
            }
            else if (collision.CompareTag(GameTags.PORTAL))
            {
                IExit exitable = collision.GetComponentInParent<IExit>();
                if (exitable != null)
                {
                    if (exitable.IsExit()) actionLevelExit?.Invoke();
                }
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
