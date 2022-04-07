using Platformer2D.Assets.Settings;
using System;
using UnityEngine;

namespace Platformer2D.Assets.Coin
{
    internal sealed class CoinFactory
    {
        private GameObject prefab;

        public CoinFactory()
        {
            prefab = Resources.Load<GameObject>(ResourcesPathes.COIN_PREFAB);
        }
        public CoinData Create()
        {
            CoinData coinData = new CoinData();

            GameObject gameObject = GameObject.Instantiate(prefab);
            CoinView view = gameObject.GetComponent<CoinView>();
            if (view == null) throw new Exception(ErrorMessages.COINVIEW_VIEW_NOT_FOUND);
            coinData.view = view;

            return coinData;
        }

        public void Destroy(CoinData coinData)
        {
            GameObject.Destroy(coinData.view.transformView.gameObject);
            coinData = null;
        }
    }
}
