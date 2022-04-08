using Platformer2D.Assets.Enemy.Cannon;
using Platformer2D.Assets.Extention;
using Platformer2D.Assets.LevelScripts;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.Coin
{
    internal sealed class CoinLevelPlacer
    {
        private Vector2 offSetPosition = new Vector2(0.5f, 0.5f);

        private LevelWallChecker levelWallChecker;
        private LevelData levelData;
        private CoinFactory coinFactory;

        public CoinLevelPlacer(LevelData levelData, CoinFactory coinFactory)
        {
            this.coinFactory = coinFactory;
            this.levelData = levelData;
            levelWallChecker = new LevelWallChecker(levelData);
        }

        public List<CoinData> Place()
        {
            List<Vector2Int> possiblePositions = GetPossiblePosition();

            List<Vector2Int> placedPositions = PlaceCoins(possiblePositions);

            List<CoinData> list = GetCoinDataList(placedPositions);

            return list;
        }

        private List<CoinData> GetCoinDataList(List<Vector2Int> positions)
        {
            List<CoinData> list = new List<CoinData>();

            foreach(Vector2Int position in positions)
            {
                CoinData coinData = coinFactory.Create();
                coinData.view.transformView.position = position + offSetPosition;
                coinData.state = CoinState.Collectable;
                list.Add(coinData);
            }

            return list;
        }

        private List<Vector2Int> PlaceCoins(List<Vector2Int> possiblePositions)
        {
            List<Vector2Int> list = new List<Vector2Int>();

            while(list.Count < levelData.levelConfig.levelConfigSettings.coinCount && possiblePositions.Count > 0)
            {
                Vector2Int possiblePosition = possiblePositions.GetRandom();
                possiblePositions.Remove(possiblePosition);

                LevelElement levelElement = levelData.levelElements[possiblePosition.x, possiblePosition.y];

                levelElement.isCoin = true;
                list.Add(possiblePosition);
            }

            return list;
        }

        private List<Vector2Int> GetPossiblePosition()
        {
            List<Vector2Int> list = new List<Vector2Int>();

            for (int x = 0; x < levelData.width; x++)
            {
                for (int y = 0; y < levelData.height; y++)
                {
                    if (PositionIsPossible(x, y)) list.Add(new Vector2Int(x, y));
                }
            }

            return list;
        }

        private bool PositionIsPossible(int x, int y)
        {
            LevelElement levelElement = levelData.levelElements[x, y];

            if (levelWallChecker.IsWall(x, y)) return false;
            if (levelElement.cannon != CannonLevelValue.None) return false;

            if (levelWallChecker.IsWall(x, y - 1)) return true;
            if (levelWallChecker.IsWall(x, y - 2)) return true;
            if (levelWallChecker.IsWall(x, y - 3)) return true;
            if (levelWallChecker.IsWall(x, y - 4)) return true;
            if (levelWallChecker.IsWall(x, y - 5)) return true;

            return false;
        }
    }
}
