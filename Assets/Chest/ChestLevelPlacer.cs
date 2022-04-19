using Platformer2D.Assets.Extention;
using Platformer2D.Assets.LevelScripts;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.Chest
{
    internal sealed class ChestLevelPlacer
    {
        private LevelData levelData;
        private LevelCoordinator levelCoordinator;
        private int countToPlace;
        private float minDistantion;

        public ChestLevelPlacer(LevelData levelData)
        {
            this.levelData = levelData;
            levelCoordinator = new LevelCoordinator(levelData);

            countToPlace = levelData.levelConfig.levelConfigSettings.chestCount;
            minDistantion = levelData.levelConfig.levelConfigSettings.chestMinDistance;
        }

        public List<Vector2Int> Place()
        {
            List<Vector2Int> possiblePosition = GetPossiblePositions();

            List<Vector2Int> choosedPositions = ChoosePositions(possiblePosition);

            SetLevelData(choosedPositions);

            return choosedPositions;
        }

        private void SetLevelData(List<Vector2Int> choosedPositions)
        {
            foreach (var position in choosedPositions)
            {
                levelData.levelElements[position.x, position.y].isChest = true;
            }
        }

        private List<Vector2Int> ChoosePositions(List<Vector2Int> possiblePositions)
        {
            List<Vector2Int> choosedPositions = new List<Vector2Int>();

            while (choosedPositions.Count < countToPlace && possiblePositions.Count > 0)
            {
                Vector2Int possiblePosition = possiblePositions.GetRandom();
                possiblePositions.Remove(possiblePosition);

                if (CheckDistance(possiblePosition, choosedPositions)) choosedPositions.Add(possiblePosition);
            }

            return choosedPositions;
        }

        private bool CheckDistance(Vector2Int possiblePosition, List<Vector2Int> choosedPositions)
        {
            foreach (Vector2Int choosedPosition in choosedPositions)
            {
                float distantion = (possiblePosition - choosedPosition).magnitude;
                if (distantion < minDistantion) return false;
            }
            return true;
        }

        private List<Vector2Int> GetPossiblePositions()
        {
            List<Vector2Int> possiblePositions = new List<Vector2Int>();

            for (int x = 0; x < levelData.width; x++)
            {
                for (int y = 0; y < levelData.height; y++)
                {
                    if (PossiblePosition(x, y)) possiblePositions.Add(new Vector2Int(x, y));
                }
            }

            return possiblePositions;
        }

        private bool PossiblePosition(int x, int y)
        {
            if (!levelCoordinator.IsEmpty(x, y)) return false;
            if (!levelCoordinator.IsEmpty(x, y + 1)) return false;
            if (!levelCoordinator.IsEmpty(x, y - 1)) return false;

            if (levelCoordinator.IsWall(x, y - 2)) return true;
            if (levelCoordinator.IsWall(x, y - 3)) return true;
            if (levelCoordinator.IsWall(x, y - 4)) return true;
            if (levelCoordinator.IsWall(x, y - 5)) return true;

            return false;
        }
    }
}
