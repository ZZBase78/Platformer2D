using Platformer2D.Assets.Interfaces;
using UnityEngine;

namespace Platformer2D.Assets.Coin
{
    internal sealed class CoinView : MonoBehaviour, ICollectable
    {
        public Transform transformView;
        public SpriteRenderer spriteRenderer;
        public CoinController coinController;
        public Collider2D colliderView;
        public Rigidbody2D rigidbodyView;

        public void Collect()
        {
            coinController.Collect();
        }
    }
}
