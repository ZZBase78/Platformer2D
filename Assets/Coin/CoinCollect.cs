using UnityEngine;
using Platformer2D.Assets.AnimationScripts;

namespace Platformer2D.Assets.Coin
{
    internal sealed class CoinCollect
    {
        private const float RANDOM_X_DIRECTION = 2f;
        private const float Y_DIRECTION = 5f;
        private const float SPEED_UP = 3f;
        private const int NEW_SPRITE_SORTING_ORDER = 10;
        private const float TIME_TO_DESTROY = 5f;
        
        private CoinData coinData;
        private AnimationData animationData;

        public CoinCollect(CoinData coinData, AnimationData animationData)
        {
            this.coinData = coinData;
            this.animationData = animationData;
        }

        public void Collect()
        {
            coinData.view.colliderView.enabled = false;
            coinData.view.rigidbodyView = coinData.view.transformView.gameObject.AddComponent<Rigidbody2D>();
            float directionX = Random.Range(-RANDOM_X_DIRECTION, RANDOM_X_DIRECTION);
            float directionY = Y_DIRECTION;
            Vector2 randomUpDirection = new Vector2(directionX, directionY);
            coinData.view.rigidbodyView.AddForce(randomUpDirection, ForceMode2D.Impulse);
            animationData.speed *= SPEED_UP;
            coinData.state = CoinState.Fall;
            coinData.view.spriteRenderer.sortingOrder = NEW_SPRITE_SORTING_ORDER;
            coinData.timeToDestroy = TIME_TO_DESTROY;
        }
    }
}
