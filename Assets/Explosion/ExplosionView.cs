using Platformer2D.Assets.AnimationScripts;
using UnityEngine;

namespace Platformer2D.Assets.Explosion
{
    internal sealed class ExplosionView : MonoBehaviour
    {
        public Transform transformView;
        public SpriteRenderer spriteRenderer;
        public AnimationTrack animationTrack;
        private AnimationController animationController;

        private void Awake()
        {
            animationController = new AnimationController(spriteRenderer);
            animationController.Play(new AnimationFactory().Create(animationTrack));
        }

        private void Update()
        {
            animationController.Update(Time.deltaTime);

            if (!animationController.isPlaying) GameObject.Destroy(gameObject);
        }
    }
}
