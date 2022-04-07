using Platformer2D.Assets.LevelScripts;
using System.Collections.Generic;

namespace Platformer2D.Assets.Coin
{
    internal sealed class CoinManager
    {
        private CoinLevelPlacer coinLevelPlacer;
        private Dictionary<CoinData, CoinController> list;

        public CoinManager(LevelData levelData)
        {
            list = new Dictionary<CoinData, CoinController>();
            coinLevelPlacer = new CoinLevelPlacer(levelData, new CoinFactory());

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
        }
    }
}
