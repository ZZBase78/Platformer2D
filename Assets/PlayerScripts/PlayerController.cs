﻿using Platformer2D.Assets.AnimationScripts;
using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerController
    {
        private Player player;
        private AnimationController animationController;
        private PlayerStateAnimation playerStateAnimation;
        private PlayerMoveController playerMoveController;
        private PlayerScaleController playerScaleController;
        private PlayerJumpController playerJumpController;
        private PlayerOnGroundController playerOnGroundController;

        public PlayerController(Player player)
        {
            this.player = player;
            animationController = new AnimationController(player.view.spriteRenderer);
            playerStateAnimation = new PlayerStateAnimation();

            playerMoveController = new PlayerMoveController(player);

            playerScaleController = new PlayerScaleController(player);
            playerJumpController = new PlayerJumpController(player);
            playerOnGroundController = new PlayerOnGroundController(player);
        }

        public void Update(float deltaTime)
        {
            playerOnGroundController.Update();

            playerMoveController.Update(deltaTime);
            playerJumpController.Update();

            playerScaleController.Update();

            animationController.Play(playerStateAnimation.GetAnimationData(player.playerState));
            animationController.Update(deltaTime);
        }
    }
}
