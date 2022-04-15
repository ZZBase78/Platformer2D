using UnityEngine;

namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelCoordinator
    {
        private LevelData levelData;

        private int width;
        private int height;

        private int cellWidth;
        private int cellHeight;
        private int wallWidth;
        private int wallHeight;

        int exitCellX;
        int exitCellY;
        int levelExitMinX;
        int levelExitMaxX;
        int levelExitMinY;
        int levelExitMaxY;

        public Vector2 worldOffSet = new Vector2(0.5f, 0.5f);

        public LevelCoordinator(LevelData levelData)
        {
            this.levelData = levelData;

            width = levelData.width;
            height = levelData.height;

            cellWidth = levelData.levelConfig.levelConfigSettings.cellWidth;
            cellHeight = levelData.levelConfig.levelConfigSettings.cellHeight;
            wallWidth = levelData.levelConfig.levelConfigSettings.wallWidth;
            wallHeight = levelData.levelConfig.levelConfigSettings.wallHeight;

            exitCellX = levelData.levelConfig.width - 1;
            exitCellY = levelData.levelConfig.height - 1;
            levelExitMinX = GetLevelLeftXFromCellX(exitCellX);
            levelExitMaxX = GetLevelRightXFromCellX(exitCellX);
            levelExitMinY = GetLevelDownYFromCellY(exitCellY);
            levelExitMaxY = GetLevelUpYFromCellY(exitCellY);
        }

        public int GetLevelLeftXFromCellX(int cellX)
        {
            return wallWidth + cellX * (cellWidth + wallWidth);
        }

        public int GetLevelRightXFromCellX(int cellX)
        {
            return wallWidth + cellX * (cellWidth + wallWidth) + cellWidth - 1;
        }

        public int GetLevelDownYFromCellY(int cellY)
        {
            return wallHeight + cellY * (cellHeight + wallHeight);
        }

        public int GetLevelUpYFromCellY(int cellY)
        {
            return wallHeight + cellY * (cellHeight + wallHeight) + cellHeight - 1;
        }

        public bool IsWall(int x, int y)
        {
            if (!CheckBounds(x, y)) return false;

            return levelData.levelElements[x, y].isWall;
        }

        private bool CheckBounds(int x, int y)
        {
            return !(x < 0 || x >= width || y < 0 || y >= height);
        }

        public bool IsExitCell(int levelX, int levelY)
        {
            return (levelX >= levelExitMinX) && (levelX <= levelExitMaxX) && (levelY >= levelExitMinY) && (levelY <= levelExitMaxY);
        }

        public bool IsEmpty(int levelX, int levelY)
        {
            if (!CheckBounds(levelX, levelY)) return false;
            return levelData.levelElements[levelX, levelY].isEmpty();
        }

        public Vector2Int GetCellFromWorld(Vector2 worldPosition)
        {
            float worldX = worldPosition.x;
            float halfCellX = (float)cellWidth / 2f;
            float offsetX = (float)wallWidth + halfCellX;
            float deviderX = (float)(wallWidth + cellWidth);
            int cellX = Mathf.RoundToInt((worldX - offsetX) / deviderX);

            float worldY = worldPosition.y;
            float halfCellY = (float)cellHeight / 2f;
            float offsetY = (float)wallHeight + halfCellY;
            float deviderY = (float)(wallHeight + cellHeight);
            int cellY = Mathf.RoundToInt((worldY - offsetY) / deviderY);

            return new Vector2Int(cellX, cellY);
        }

        public Vector2Int GetLevelFromWorld(Vector2 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.x - worldOffSet.x);
            int y = Mathf.RoundToInt(worldPosition.y - worldOffSet.y);
            return new Vector2Int(x, y);
        }
    }
}
