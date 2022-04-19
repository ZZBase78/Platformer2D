using Platformer2D.Assets.Settings;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer2D.Assets.LevelScripts.Background
{
    internal sealed class BackgroundController
    {
        private Tilemap backgroundTileMap;
        private BackgroundTileSet backgroundTileSet;
        private BackgroundGenerator backgroundGenerator;

        public BackgroundController(LevelData levelData)
        {
            GameObject levelGrid = GameObject.Find(SceneObjectNames.LEVEL_GRID);
            if (levelGrid == null)
            {
                GameObject levelGridPrefab = Resources.Load<GameObject>(ResourcesPathes.LEVEL_GRID);
                levelGrid = GameObject.Instantiate(levelGridPrefab);
                levelGrid.name = SceneObjectNames.LEVEL_GRID;
            }

            GameObject backgroundTilemapGameObject = GameObject.Find(SceneObjectNames.BACKGROUND_TILEMAP);
            if (backgroundTilemapGameObject == null)
            {
                GameObject backgroundTilemapPrefab = Resources.Load<GameObject>(ResourcesPathes.BACKGROUND_TILEMAP);
                backgroundTilemapGameObject = GameObject.Instantiate(backgroundTilemapPrefab);
                backgroundTilemapGameObject.name = SceneObjectNames.BACKGROUND_TILEMAP;
                backgroundTilemapGameObject.transform.SetParent(levelGrid.transform);
            }

            backgroundTileMap = backgroundTilemapGameObject.GetComponent<Tilemap>();
            if (backgroundTileMap == null) throw new Exception(ErrorMessages.BACKGROUND_TILEMAP_NOT_FOUND);

            backgroundTileSet = Resources.Load<BackgroundTileSet>(ResourcesPathes.BACKGROUND_TILESET);

            backgroundGenerator = new BackgroundGenerator(levelData);
        }

        public void Generate()
        {
            int[,] backgroundMap = backgroundGenerator.Generate();
            DisplayBackground(backgroundMap);
        }

        private void DisplayBackground(int[,] backgroundMap)
        {
            for (int x = 0; x < backgroundMap.GetLength(0); x++)
            {
                for (int y = 0; y < backgroundMap.GetLength(1); y++)
                {
                    if (backgroundMap[x, y] == BackgroundMapValues.WALL_VALUE)
                    {
                        backgroundTileMap.SetTile(new Vector3Int(x, y, 0), backgroundTileSet.Wall);
                    }
                    
                }
            }
        }
    }
}
