using Platformer2D.Assets.AnimationScripts;
using UnityEngine;

namespace Platformer2D.Assets.Coin
{
    internal sealed class CoinController
    {
        private const int ANIMATION_RANDOM_RANGE_VALUE = 5;

        private CoinData coinData;
        private AnimationController animationController;

        public CoinController(CoinData coinData)
        {
            this.coinData = coinData;
            animationController = new AnimationController(coinData.view.spriteRenderer);
            AnimationData animationData = new AnimationFactory().Create(ResourcesAnimationPathes.COIN_IDLE);
            animationData.speed = Random.Range(animationData.speed - ANIMATION_RANDOM_RANGE_VALUE, animationData.speed + ANIMATION_RANDOM_RANGE_VALUE);
            animationController.Play(animationData);
        }

        public void Update(float deltaTime)
        {
            animationController.Update(deltaTime);
        }
    }
}
