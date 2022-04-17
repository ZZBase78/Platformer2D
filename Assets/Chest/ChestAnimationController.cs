using Platformer2D.Assets.AnimationScripts;
using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.Chest
{
    internal sealed class ChestAnimationController
    {
        private const float MIN_VALUE = 0.01f;
        private Rigidbody2D rigidBody2D;
        private AnimationController animationController;
        private AnimationData idleAnimationData;
        private AnimationData openAnimationData;
        private AnimationData closeAnimationData;
        private ChestAnimationState chestAnimationState;

        public ChestAnimationController(Rigidbody2D rigidBody2D, SpriteRenderer spriteRenderer)
        {
            this.rigidBody2D = rigidBody2D;
            this.animationController = new AnimationController(spriteRenderer);

            idleAnimationData = new AnimationFactory().Create(ResourcesPathes.CHEST_IDLE_ANIMATION);
            openAnimationData = new AnimationFactory().Create(ResourcesPathes.CHEST_OPEN_ANIMATION);
            closeAnimationData = new AnimationFactory().Create(ResourcesPathes.CHEST_CLOSE_ANIMATION);

            chestAnimationState = ChestAnimationState.Idle;
            animationController.Play(idleAnimationData);
        }

        public void Update(float deltaTime)
        {
            float yVelocity = rigidBody2D.velocity.y;
            float ABSyVelocity = Mathf.Abs(yVelocity);

            if (chestAnimationState == ChestAnimationState.Idle)
            {
                if (yVelocity < -MIN_VALUE)
                {
                    chestAnimationState = ChestAnimationState.Opening;
                    animationController.Play(openAnimationData);
                }
            }
            else if (chestAnimationState == ChestAnimationState.Opening)
            {
                if (yVelocity > -MIN_VALUE && !animationController.isPlaying)
                {
                    chestAnimationState = ChestAnimationState.Closing;
                    animationController.Play(closeAnimationData);
                }
            }
            else if (chestAnimationState == ChestAnimationState.Closing)
            {
                if (ABSyVelocity < MIN_VALUE && !animationController.isPlaying)
                {
                    chestAnimationState = ChestAnimationState.Idle;
                    animationController.Play(idleAnimationData);
                }
            }

            animationController.Update(deltaTime);
        }
    }
}
