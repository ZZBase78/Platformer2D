using Platformer2D.Assets.Settings;
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
        private LevelCoordinator levelCoordinator;

        public LevelWallTileCalulator(LevelData levelData)
        {
            this.levelData = levelData;
            this.width = levelData.width;
            this.height = levelData.height;
            levelCoordinator = new LevelCoordinator(levelData);
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
                        bool up = levelCoordinator.IsWall(x, y + 1);
                        bool down = levelCoordinator.IsWall(x, y - 1);
                        bool left = levelCoordinator.IsWall(x - 1, y);
                        bool right = levelCoordinator.IsWall(x + 1, y);
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

    }
}
