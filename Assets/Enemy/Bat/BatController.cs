using Platformer2D.Assets.AnimationScripts;
using Platformer2D.Assets.Settings;

namespace Platformer2D.Assets.Enemy.Bat
{
    internal sealed class BatController
    {
        private const float RANDOM_ANIMATION_SPEED_DEVIATION = 10f;
        private BatData data;
        private AnimationController animationController;

        public BatController(BatData _data)
        {
            data = _data;

            animationController = new AnimationController(data.view.spriteRenderer);
            animationController.Play(new AnimationFactory().CreateRandomSpeedDeviation(ResourcesPathes.BAT_FLY_ANNIMATION_TRACK, RANDOM_ANIMATION_SPEED_DEVIATION));
        }

        public void Update(float deltaTime)
        {
            animationController.Update(deltaTime);
        }
    }
}
