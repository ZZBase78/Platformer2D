using UnityEngine;

namespace Platformer2D.Assets.CollidersScripts
{
    internal sealed class CollidersController
    {
        private int maxColliders;
        private Collider2D[] colliders;

        public CollidersController(int maxcolliders)
        {
            this.maxColliders = maxcolliders;
            colliders = new Collider2D[maxColliders];
        }
        public Collider2D CheckTag(string tag, Vector2 point, Vector2 size, float angle)
        {
            int resultCount = Physics2D.OverlapBoxNonAlloc(point, size, angle, colliders);
            if (resultCount == 0) return null;

            for (int i = 0; i < resultCount; i++)
            {
                Collider2D collider = colliders[i];
                if (collider.CompareTag(tag))
                {
                    return collider;
                }
            }

            return null;
        }
    }
}
