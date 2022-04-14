using Platformer2D.Assets.AnimationScripts;
using Platformer2D.Assets.Settings;

namespace Platformer2D.Assets.Enemy.Bat
{
    internal sealed class BatController
    {
        private BatData data;
        private AnimationController animationController;

        public BatController(BatData _data)
        {
            data = _data;
            animationController.Play(new AnimationFactory().Create(ResourcesPathes.BAT_FLY_ANNIMATION_TRACK));
        }

        public void Update(float deltaTime)
        {
            animationController.Update(deltaTime);
        }
    }
}
