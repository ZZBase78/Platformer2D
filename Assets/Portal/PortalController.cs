using Platformer2D.Assets.AnimationScripts;

namespace Platformer2D.Assets.Portal
{
    internal sealed class PortalController
    {
        private PortalData portalData;
        private AnimationController animationController;

        public PortalController(PortalData portalData)
        {
            this.portalData = portalData;
            portalData.view.portalController = this;
            animationController = new AnimationController(portalData.view.spriteRenderer);
            animationController.Play(new AnimationFactory().Create(portalData.model.animation));
        }

        public void Update(float deltatime)
        {
            animationController.Update(deltatime);
        }

        public bool IsExit()
        {
            return portalData.IsExit;
        }
    }
}
