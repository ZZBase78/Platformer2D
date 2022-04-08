using Platformer2D.Assets.AnimationScripts;
using UnityEngine;

namespace Platformer2D.Assets.Coin
{
    internal sealed class CoinController
    {
        private const int ANIMATION_RANDOM_RANGE_VALUE = 5;

        private CoinData coinData;
        private AnimationController animationController;
        private AnimationData animationData;
        private CoinFallController coinFallController;

        public CoinController(CoinData coinData)
        {
            this.coinData = coinData;
            coinData.view.coinController = this;

            animationController = new AnimationController(coinData.view.spriteRenderer);
            animationData = new AnimationFactory().Create(ResourcesAnimationPathes.COIN_IDLE);
            animationData.speed = Random.Range(animationData.speed - ANIMATION_RANDOM_RANGE_VALUE, animationData.speed + ANIMATION_RANDOM_RANGE_VALUE);
            animationController.Play(animationData);
            coinFallController = new CoinFallController(coinData);
        }

        public void Collect()
        {
            new CoinCollect(coinData, animationData).Collect();
        }

        public void Update(float deltaTime)
        {
            animationController.Update(deltaTime);
            coinFallController.Update(deltaTime);
        }
    }
}
