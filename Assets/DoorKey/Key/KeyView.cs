using Platformer2D.Assets.Interfaces;
using UnityEngine;

namespace Platformer2D.Assets.DoorKey.Key
{
    internal sealed class KeyView : MonoBehaviour, ICollectable
    {
        public Transform transformView;
        public SpriteRenderer spriteRenderer;
        public KeyController keyController;
        public BoxCollider2D colliderView;

        public void Collect()
        {
            keyController.Collect();
        }
    }
}
