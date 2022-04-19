using Platformer2D.Assets.Settings;
using System;
using UnityEngine;

namespace Platformer2D.Assets.LevelScripts.Background
{
    internal sealed class BackgroundGenerator
    {
        private delegate void LevelAction(int[,] map, int x, int y);
        private delegate void TargetLevelAction(int[,] sourceMap, int[,] targetMap, int x, int y);

        private LevelData levelData;

        private int[,] map;
        private int[,] nextMap;

        private int width;
        private int height;

        private BackgroundGeneratorSettings settings;
        private LevelCoordinator levelCoordinator;

        public BackgroundGenerator(LevelData levelData)
        {
            this.levelData = levelData;

            width = levelData.width;
            height = levelData.height;

            map = new int[width, height];
            nextMap = new int[width, height];

            settings = Resources.Load<BackgroundGeneratorSettings>(ResourcesPathes.BACKGROUND_GENERATOR_SETTINGS);

            levelCoordinator = new LevelCoordinator(levelData);
        }

        private void MapByPass(int[,] map, LevelAction action)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    action.Invoke(map, x, y);
                }
            }
        }

        private void TargetMapByPass(int[,] sourceMap, int[,] targetMap, TargetLevelAction action)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    action.Invoke(sourceMap, targetMap, x, y);
                }
            }
        }

        private bool IsBorder(int x, int y)
        {
            return (x == 0 || x == width - 1 || y == 0 || y == height - 1);
        }

        private void FillCell(int[,] map, int x, int y)
        {
            if (IsBorder(x, y))
            {
                map[x, y] = BackgroundMapValues.WALL_VALUE;
                return;
            }
            int randomValue = UnityEngine.Random.Range(0, 101);
            map[x, y] = randomValue <= settings.fillPercent ? BackgroundMapValues.WALL_VALUE : BackgroundMapValues.EMPTY_VALUE;
        }

        private int GetNeighbourWall(int[,] map, int x, int y)
        {
            int count = 0;
            count += map[x - 1, y + 1] == BackgroundMapValues.WALL_VALUE ? 1 : 0;
            count += map[x, y + 1] == BackgroundMapValues.WALL_VALUE ? 1 : 0;
            count += map[x + 1, y + 1] == BackgroundMapValues.WALL_VALUE ? 1 : 0;
            count += map[x - 1, y] == BackgroundMapValues.WALL_VALUE ? 1 : 0;
            count += map[x + 1, y] == BackgroundMapValues.WALL_VALUE ? 1 : 0;
            count += map[x - 1, y - 1] == BackgroundMapValues.WALL_VALUE ? 1 : 0;
            count += map[x, y - 1] == BackgroundMapValues.WALL_VALUE ? 1 : 0;
            count += map[x + 1, y - 1] == BackgroundMapValues.WALL_VALUE ? 1 : 0;
            return count;
        }

        private void SmoothCell(int[,] sourceMap, int[,] targetMap, int x, int y)
        {
            if (IsBorder(x, y))
            {
                targetMap[x, y] = sourceMap[x, y];
                return;
            }
            int countNeighbour = GetNeighbourWall(sourceMap, x, y);
            if (countNeighbour >= settings.wallNeighbour)
            {
                targetMap[x, y] = BackgroundMapValues.WALL_VALUE;
            }
            else if (countNeighbour <= settings.emptyNeighbour)
            {
                targetMap[x, y] = BackgroundMapValues.EMPTY_VALUE;
            }

        }

        private void CopyCell(int[,] sourceMap, int[,] targetMap, int x, int y)
        {
            targetMap[x, y] = sourceMap[x, y];
        }

        private void ClearUnusableWalls(int[,] map, int x, int y)
        {
            if (levelCoordinator.IsWall(x, y)) map[x, y] = BackgroundMapValues.EMPTY_VALUE;
        }

        public int[,] Generate()
        {
            MapByPass(map, FillCell);
            for (int i = 0; i < settings.smoothCount; i++)
            {
                TargetMapByPass(map, nextMap, SmoothCell);
                TargetMapByPass(nextMap, map, CopyCell);
            }
            MapByPass(map, ClearUnusableWalls);
            return map;
        }

    }
}
