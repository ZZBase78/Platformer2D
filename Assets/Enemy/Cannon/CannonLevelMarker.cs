using Platformer2D.Assets.LevelScripts;
using System.Collections.Generic;

namespace Platformer2D.Assets.Enemy.Cannon
{
    internal sealed class CannonLevelMarker
    {
        public void MarkCannonsOnLevel(LevelData levelData, List<CannonData> cannons)
        {
            foreach(CannonData cannon in cannons)
            {
                MarkCannonOnLevel(levelData, cannon);
            }
        }
        private void MarkCannonOnLevel(LevelData levelData, CannonData cannon)
        {
            MarkLevel(levelData, cannon.levelX, cannon.levelY, CannonLevelValue.Center);

            MarkLevel(levelData, cannon.levelX - 1, cannon.levelY + 1, CannonLevelValue.Near);
            MarkLevel(levelData, cannon.levelX, cannon.levelY + 1, CannonLevelValue.Near);
            MarkLevel(levelData, cannon.levelX + 1, cannon.levelY + 1, CannonLevelValue.Near);

            MarkLevel(levelData, cannon.levelX - 1, cannon.levelY, CannonLevelValue.Near);
            MarkLevel(levelData, cannon.levelX + 1, cannon.levelY, CannonLevelValue.Near);

            MarkLevel(levelData, cannon.levelX - 1, cannon.levelY - 1, CannonLevelValue.Near);
            MarkLevel(levelData, cannon.levelX, cannon.levelY - 1, CannonLevelValue.Near);
            MarkLevel(levelData, cannon.levelX + 1, cannon.levelY - 1, CannonLevelValue.Near);
        }

        private void MarkLevel(LevelData levelData, int x, int y, CannonLevelValue value)
        {
            if (x < 0 || x >= levelData.width || y < 0 || y >= levelData.height) return;

            levelData.levelElements[x, y].cannon = value;
        }
    }
}
