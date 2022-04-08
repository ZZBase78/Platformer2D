namespace Platformer2D.Assets.Coin
{
    internal sealed class CoinFallController
    {
        private CoinData coinData;

        public CoinFallController(CoinData coinData)
        {
            this.coinData = coinData;
        }

        public void Update(float deltaTime)
        {
            if (coinData.state != CoinState.Fall) return;

            coinData.timeToDestroy -= deltaTime;
            if (coinData.timeToDestroy <= 0) coinData.state = CoinState.Destroy;
        }
    }
}
