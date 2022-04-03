﻿using Platformer2D.Assets.Settings;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelWallTileCalulator
    {
        private LevelData levelData;
        private int width;
        private int height;
        private LevelWallsTilesSet levelWallsTilesSet;

        public LevelWallTileCalulator(LevelData levelData)
        {
            this.levelData = levelData;
            this.width = levelData.width;
            this.height = levelData.height;
        }

        public void Calulate()
        {
            levelWallsTilesSet = Resources.Load<LevelWallsTilesSet>(ResourcesPathes.LEVEL_WALLS_TILES_SET);

            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (levelData.levelElements[x, y].isWall)
                    {
                        bool up = IsWall(x, y + 1);
                        bool down = IsWall(x, y - 1);
                        bool left = IsWall(x - 1, y);
                        bool right = IsWall(x + 1, y);
                        Tile tile = GetTileByHearWalls(up, down, left, right);
                        if (tile != null)
                        {
                            levelData.levelElements[x, y].isTiled = true;
                            levelData.levelElements[x, y].tile = tile;
                        }
                    }
                }

            }
        }

        private Tile GetTileByHearWalls(bool up, bool down, bool left, bool right)
        {
            if (!up && !down && !left && !right) return levelWallsTilesSet.wallTile;
            if (!up && !down && !left && right) return levelWallsTilesSet.wallTileRight;
            if (!up && !down && left && !right) return levelWallsTilesSet.wallTileLeft;
            if (!up && !down && left && right) return levelWallsTilesSet.wallTileRightLeft;
            if (!up && down && !left && !right) return levelWallsTilesSet.wallTileDown;
            if (!up && down && !left && right) return levelWallsTilesSet.wallTileRightDown;
            if (!up && down && left && !right) return levelWallsTilesSet.wallTileLeftDown;
            if (!up && down && left && right) return levelWallsTilesSet.wallTileRightLeftDown;
            if (up && !down && !left && !right) return levelWallsTilesSet.wallTileUp;
            if (up && !down && !left && right) return levelWallsTilesSet.wallTileRightUp;
            if (up && !down && left && !right) return levelWallsTilesSet.wallTileLeftUp;
            if (up && !down && left && right) return levelWallsTilesSet.wallTileRightLeftUp;
            if (up && down && !left && !right) return levelWallsTilesSet.wallTileUpDown;
            if (up && down && !left && right) return levelWallsTilesSet.wallTileRightUpDown;
            if (up && down && left && !right) return levelWallsTilesSet.wallTileLeftUpDown;
            if (up && down && left && right) return levelWallsTilesSet.wallTileRightLeftUpDown;

            return null;
        }

        private bool IsWall(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height) return false;

            return levelData.levelElements[x, y].isWall;
        }
    }
}
