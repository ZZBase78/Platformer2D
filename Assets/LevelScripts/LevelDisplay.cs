using Platformer2D.Assets.Settings;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelDisplay
    {

        private Tilemap wallTileMap;

        public LevelDisplay()
        {
            GameObject levelGrid = GameObject.Find(SceneObjectNames.LEVEL_GRID);
            if (levelGrid == null)
            {
                GameObject levelGridPrefab = Resources.Load<GameObject>(ResourcesPathes.LEVEL_GRID);
                levelGrid = GameObject.Instantiate(levelGridPrefab);
                levelGrid.name = SceneObjectNames.LEVEL_GRID;
            }

            GameObject wallTilemapGameObject = GameObject.Find(SceneObjectNames.WALL_TILEMAP);
            if (wallTilemapGameObject == null)
            {
                GameObject wallTilemapPrefab = Resources.Load<GameObject>(ResourcesPathes.WALL_TILEMAP);
                wallTilemapGameObject = GameObject.Instantiate(wallTilemapPrefab);
                wallTilemapGameObject.name = SceneObjectNames.WALL_TILEMAP;
                wallTilemapGameObject.transform.SetParent(levelGrid.transform);
            }

            wallTileMap = wallTilemapGameObject.GetComponent<Tilemap>();
            if (wallTileMap == null) throw new Exception(ErrorMessages.TILEMAP_NOT_FOUND);
        }

        public void Display(LevelData levelData)
        {
            int width = levelData.width;
            int height = levelData.height;

            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    LevelElement levelElement = levelData.levelElements[x, y];
                    if (levelElement.isWall && levelElement.isTiled)
                    {
                        Vector3Int position = new Vector3Int(x, y, 0);
                        wallTileMap.SetTile(position, levelElement.tile);
                    }
                }
            }
        }
    }
}
