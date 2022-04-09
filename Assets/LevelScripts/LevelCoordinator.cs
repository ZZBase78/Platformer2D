﻿using UnityEngine;

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
    }
}
