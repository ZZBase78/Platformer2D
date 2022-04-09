
using Platformer2D.Assets.LevelScripts;
using System.Collections.Generic;

namespace Platformer2D.Assets.Portal
{
    internal sealed class PortalManager
    {
        private Dictionary<PortalData, PortalController> dictionary;
        private PortalFactory portalFactory;
        private LevelData levelData;

        public PortalManager(LevelData levelData)
        {
            dictionary = new Dictionary<PortalData, PortalController>();
            portalFactory = new PortalFactory();
            this.levelData = levelData;
        }

        public void Update(float deltaTime)
        {
            foreach(var element in dictionary)
            {
                element.Value.Update(deltaTime);
            }
        }

        private void AddPortal(PortalData portalData)
        {
            dictionary.Add(portalData, new PortalController(portalData));
        }

        public void CreateExitPortal()
        {
            PortalData portalData = portalFactory.Create();
            new PortalSetExit(levelData, portalData).Set();

            AddPortal(portalData);
        }
    }
}
