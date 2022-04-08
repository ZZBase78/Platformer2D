using UnityEngine;

namespace Platformer2D.Assets.AnimationScripts
{
    internal sealed class AnimationController
    {
        private SpriteRenderer spriteRenderer;
        private AnimationData animationData;
        public bool isPlaying;

        public AnimationController(SpriteRenderer spriteRenderer)
        {
            this.spriteRenderer = spriteRenderer;
            isPlaying = false;
        }

        public void Play(AnimationData animationData)
        {
            if (this.animationData != animationData)
            {
                this.animationData = animationData;
                this.animationData.counter = 0;
            }
            
            isPlaying = true;
        }
        public void Stop()
        {
            isPlaying = false;
        }

        private void PlayAnimation(float deltaTime)
        {
            if (animationData == null)
            {
                Stop();
                return;
            }

            if (animationData.track.Count == 0)
            {
                Stop();
                return;
            }

            animationData.counter += deltaTime * animationData.speed;
            if (animationData.loop)
            {
                while(animationData.counter >= animationData.track.Count)
                {
                    animationData.counter -= animationData.track.Count;
                }
            }
            else if (animationData.counter >= animationData.track.Count)
            {
                animationData.counter = animationData.track.Count - 1;
                Stop();
            }

            spriteRenderer.sprite = animationData.track[(int)animationData.counter];
        }

        public void Update(float deltaTime)
        {
            if (isPlaying) PlayAnimation(deltaTime);
        }
    }
}
