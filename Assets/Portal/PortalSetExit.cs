
using Platformer2D.Assets.LevelScripts;
using UnityEngine;

namespace Platformer2D.Assets.Portal
{
    internal sealed class PortalSetExit
    {
        private LevelData levelData;
        private LevelCoordinator levelCoordinator;
        private PortalData portalData;

        public PortalSetExit(LevelData levelData, PortalData portalData)
        {
            this.levelData = levelData;
            this.portalData = portalData;
            levelCoordinator = new LevelCoordinator(levelData);
        }

        public void Set()
        {
            portalData.IsExit = true;

            int exitPointX = levelCoordinator.GetLevelRightXFromCellX(levelData.levelConfig.width - 1) - 1;
            int exitPointY = levelCoordinator.GetLevelDownYFromCellY(levelData.levelConfig.height - 1) + 1;

            Vector2 exitPortalPosition = new Vector2(exitPointX, exitPointY) + levelCoordinator.worldOffSet;

            portalData.view.transformView.position = exitPortalPosition;
        }
    }
}
