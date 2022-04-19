using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.World.Background
{
    internal sealed class WorldBackgroundController
    {
        private Transform transform;
        private GameObject gameObject;
        private SpriteRenderer spriteRenderer;
        private Camera camera;

        public WorldBackgroundController()
        {
            GameObject prefab = Resources.Load<GameObject>(ResourcesPathes.WORLD_BACKGROUND_PREFAB);

            gameObject = GameObject.Instantiate(prefab);
            transform = gameObject.transform;

            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            camera = Camera.main;
        }

        public void Update()
        {
            float cameraHeight = camera.orthographicSize * 2;
            Vector2 cameraSize = new Vector2(camera.aspect * cameraHeight, cameraHeight);
            Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

            Vector2 scale = Vector2.one;

            if (cameraSize.x >= cameraSize.y)
            {
                scale *= cameraSize.x / spriteSize.x;
            }
            else
            {
                scale *= cameraSize.y / spriteSize.y;
            }

            Vector3 position = camera.transform.position;
            position.z = 0;
            transform.position = position;
            transform.localScale = scale;
        }
    }
}
