using Platformer2D.Assets.LevelScripts;
using Platformer2D.Assets.Quest;
using Platformer2D.Assets.Settings;
using System;
using UnityEngine;

namespace Platformer2D.Assets.DoorKey.Key
{
    internal sealed class KeyController : IEventQuestCollect
    {
        private const float TARGET_ZOOM = 10f;
        private const float SPEED_ZOOM = 5f;
        private const float TARGET_ALPHA = 0f;
        private const float SPEED_ALPHA = 0.5f;
        private const int ZOOM_SORT = 1000;
        private const float SPEED_CENTER_CAMERA = 100f;

        private KeyData data;
        private LevelData levelData;
        private LevelCoordinator levelCoordinator;

        public event Action actionQuestCollect;

        private KeyState state;

        private Transform cameraTransform;
        private float center_screen_time;

        public KeyController(LevelData levelData)
        {
            this.levelData = levelData;
            levelCoordinator = new LevelCoordinator(levelData);

            GameObject prefab = Resources.Load<GameObject>(ResourcesPathes.KEY_PREFAB);
            GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
            KeyView view = gameObject.GetComponentInChildren<KeyView>();
            view.keyController = this;

            data = new KeyData();
            data.view = view;

            MoveRandom();

            state = KeyState.Idle;

            cameraTransform = Camera.main.transform;

            center_screen_time = 0;
        }

        public void Collect()
        {
            actionQuestCollect?.Invoke();
            state = KeyState.Zooming;
            data.view.spriteRenderer.sortingOrder = ZOOM_SORT;
            data.view.colliderView.enabled = false;
        }

        private void SetCenterScreen(float deltaTime)
        {
            center_screen_time += deltaTime / SPEED_CENTER_CAMERA;
            if (center_screen_time > 1f) center_screen_time = 1f;
            Vector3 targetPosition = new Vector3(cameraTransform.position.x, cameraTransform.position.y, 0f);
            Vector3 currentPosition = data.view.transformView.position;
            currentPosition = Vector3.Lerp(currentPosition, targetPosition, center_screen_time);
            data.view.transformView.position = currentPosition;

        }

        public void Zoom(float deltaTime)
        {
            float currentScale = data.view.transformView.localScale.x;
            float newScale = Mathf.MoveTowards(currentScale, TARGET_ZOOM, SPEED_ZOOM * deltaTime);
            Vector3 newTransformScale = new Vector3(newScale, newScale, 1f);
            data.view.transformView.localScale = newTransformScale;

            SetCenterScreen(deltaTime);

            if (Mathf.Approximately(newScale, TARGET_ZOOM)) state = KeyState.Disappearing;
        }

        public void Disappear(float deltaTime)
        {
            float currentAlpha = data.view.spriteRenderer.color.a;
            float newAlpha = Mathf.MoveTowards(currentAlpha, TARGET_ALPHA, SPEED_ALPHA * deltaTime);

            Color color = data.view.spriteRenderer.color;
            color.a = newAlpha;
            data.view.spriteRenderer.color = color;

            SetCenterScreen(deltaTime);

            if (Mathf.Approximately(newAlpha, TARGET_ALPHA)) state = KeyState.Destroyed;
        }

        public void Update(float deltaTime)
        {
            if (state == KeyState.Idle)
            {
                return;
            }
            else if (state == KeyState.Destroyed)
            {
                return;
            }
            else if (state == KeyState.Zooming)
            { 
                Zoom(deltaTime); 
            }
            else if (state == KeyState.Disappearing)
            {
                Disappear(deltaTime);
            }
        }

        private bool PossiblePosition(int x, int y)
        {
            if (levelCoordinator.IsExitCell(x, y)) return false;
            if (!levelCoordinator.IsEmpty(x, y)) return false;
            if (levelCoordinator.IsWall(x, y - 1)) return true;
            if (levelCoordinator.IsWall(x, y - 2)) return true;
            if (levelCoordinator.IsWall(x, y - 3)) return true;
            if (levelCoordinator.IsWall(x, y - 4)) return true;
            if (levelCoordinator.IsWall(x, y - 5)) return true;
            return false;
        }

        private void MoveRandom()
        {
            int levelX = 0;
            int levelY = 0;
            do
            {
                levelX = UnityEngine.Random.Range(0, levelData.width);
                levelY = UnityEngine.Random.Range(0, levelData.height);

            } while (!PossiblePosition(levelX, levelY));

            Vector3 keyPosition = new Vector3(levelX, levelY, 0) + (Vector3)levelCoordinator.worldOffSet;
            data.view.transformView.position = keyPosition;
        }
    }
}
