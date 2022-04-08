using Platformer2D.Assets.LevelScripts;
using System.Collections.Generic;

namespace Platformer2D.Assets.Coin
{
    internal sealed class CoinManager
    {
        private CoinLevelPlacer coinLevelPlacer;
        private Dictionary<CoinData, CoinController> list;
        private CoinFactory coinFactory;
        private List<CoinData> destroyList;

        public CoinManager(LevelData levelData)
        {
            destroyList = new List<CoinData>();
            list = new Dictionary<CoinData, CoinController>();
            coinFactory = new CoinFactory();
            coinLevelPlacer = new CoinLevelPlacer(levelData, coinFactory);

            Generate();
        }

        private void Generate()
        {
            List<CoinData> listCoinData = coinLevelPlacer.Place();

            foreach(CoinData coinData in listCoinData)
            {
                list.Add(coinData, new CoinController(coinData));
            }
        }

        public void Update(float deltaTime)
        {
            foreach (var element in list)
            {
                element.Value.Update(deltaTime);
            }
            CheckDestroy();
        }

        private void CheckDestroy()
        {
            destroyList.Clear();
            foreach (var element in list)
            {
                if (element.Key.state == CoinState.Destroy) destroyList.Add(element.Key);
            }
            foreach(CoinData coinData in destroyList)
            {
                coinFactory.Destroy(coinData);
                list.Remove(coinData);
            }
        }
    }
}
