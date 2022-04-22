using Platformer2D.Assets.LevelScripts;
using Platformer2D.Assets.Quest;
using Platformer2D.Assets.Settings;
using UnityEngine;

namespace Platformer2D.Assets.DoorKey.Door
{
    internal sealed class DoorController : IQuestAction
    {
        private const float TARGET_ALPHA = 0f;
        private const float SPEED_ALPHA = 0.5f;

        private DoorData data;
        private LevelData levelData;
        private LevelCoordinator levelCoordinator;

        private DoorState state;
        public DoorController(LevelData levelData)
        {
            this.levelData = levelData;
            levelCoordinator = new LevelCoordinator(levelData);

            GameObject prefab = Resources.Load<GameObject>(ResourcesPathes.DOOR_PREFAB);
            GameObject gameObject = Object.Instantiate(prefab);
            DoorView view = gameObject.GetComponentInChildren<DoorView>();

            data = new DoorData();
            data.view = view;

            MoveToExit();

            state = DoorState.Idle;
        }

        public void OpenDoor()
        {
            state = DoorState.Disappearing;
            data.view.colliderView.enabled = false;
        }

        public void Disappear(float deltaTime)
        {
            float currentAlpha = data.view.spriteRenderer.color.a;
            float newAlpha = Mathf.MoveTowards(currentAlpha, TARGET_ALPHA, SPEED_ALPHA * deltaTime);

            Color color = data.view.spriteRenderer.color;
            color.a = newAlpha;
            data.view.spriteRenderer.color = color;

            if (Mathf.Approximately(newAlpha, TARGET_ALPHA)) state = DoorState.Destroyed;
        }

        public void Update(float deltaTime)
        {
            if (state == DoorState.Idle)
            {
                return;
            }
            else if (state == DoorState.Destroyed)
            {
                return;
            }
            else if (state == DoorState.Disappearing)
            {
                Disappear(deltaTime);
            }
        }

        public void QuestAction()
        {
            OpenDoor();
        }

        private void MoveToExit()
        {
            int levelX = levelCoordinator.GetLevelRightXFromCellX(levelData.levelConfig.width - 1) - 3;
            int levelY = levelCoordinator.GetLevelDownYFromCellY(levelData.levelConfig.height - 1) + 1;
            Vector3 exitDoorPosition = new Vector3(levelX, levelY, 0) + (Vector3)levelCoordinator.worldOffSet;
            data.view.transformView.position = exitDoorPosition;
        }
    }
}
