using Platformer2D.Assets.LevelScripts;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.Chest
{
    internal sealed class ChestManager
    {
        private Dictionary<ChestData, ChestController> dictionary;
        private List<ChestData> addList;
        private List<ChestData> removeList;
        private ChestFactory factory;

        private LevelData levelData;
        private LevelCoordinator levelCoordinator;
        private ChestLevelPlacer chestLevelPlacer;

        public ChestManager(LevelData levelData)
        {
            this.levelData = levelData;
            dictionary = new Dictionary<ChestData, ChestController>();
            addList = new List<ChestData>();
            removeList = new List<ChestData>();
            factory = new ChestFactory();

            chestLevelPlacer = new ChestLevelPlacer(levelData);
            levelCoordinator = new LevelCoordinator(levelData);
        }

        public void Update(float deltaTime)
        {
            AddElements();
            RemoveElements();
            foreach (var keyValue in dictionary)
            {
                keyValue.Value.Update(deltaTime);
            }
        }

        private void AddElements()
        {
            foreach (ChestData data in addList)
            {
                dictionary.Add(data, new ChestController(data));
            }
            addList.Clear();
        }

        private void RemoveElements()
        {
            foreach (ChestData data in removeList)
            {
                dictionary.Remove(data);
                Destroy(data);
            }
            removeList.Clear();
        }

        private void AddElement(ChestData data)
        {
            addList.Add(data);
        }

        private void RemoveElement(ChestData data)
        {
            removeList.Add(data);
        }

        private void Destroy(ChestData data)
        {
            factory.Destroy(data);
            data = null;
        }

        public void PlaceRandom()
        {
            List<Vector2Int> placedPositions = chestLevelPlacer.Place();

            foreach (Vector2Int position in placedPositions)
            {
                ChestData chestData = factory.Create();
                chestData.view.transformView.position = position + levelCoordinator.worldOffSet;
                AddElement(chestData);
            }
        }
    }
}
