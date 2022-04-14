using Platformer2D.Assets.LevelScripts;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.Assets.Enemy.Bat
{
    internal sealed class BatManager
    {
        private Dictionary<BatData, BatController> dictionary;
        private List<BatData> addList;
        private List<BatData> removeList;
        private BatFactory factory;

        private BatLevelPlacer batLevelPlacer;
        private LevelData levelData;
        private LevelCoordinator levelCoordinator;

        public BatManager(LevelData _levelData)
        {
            levelData = _levelData;

            dictionary = new Dictionary<BatData, BatController>();
            addList = new List<BatData>();
            removeList = new List<BatData>();
            factory = new BatFactory();
            batLevelPlacer = new BatLevelPlacer(levelData);
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
            foreach (BatData data in addList)
            {
                dictionary.Add(data, new BatController(data));
            }
            addList.Clear();
        }

        private void RemoveElements()
        {
            foreach (BatData data in removeList)
            {
                dictionary.Remove(data);
                Destroy(data);
            }
            removeList.Clear();
        }

        private void AddElement(BatData data)
        {
            addList.Add(data);
        }

        private void RemoveElement(BatData data)
        {
            removeList.Add(data);
        }

        private void Destroy(BatData data)
        {
            factory.Destroy(data);
            data = null;
        }

        public void PlaceRandom()
        {
            List<Vector2Int> placedPositions = batLevelPlacer.Place();

            foreach (Vector2Int position in placedPositions)
            {
                BatData batData = factory.Create();
                batData.view.transformView.position = position + levelCoordinator.worldOffSet;
                AddElement(batData);
            }
        }
    }
}
