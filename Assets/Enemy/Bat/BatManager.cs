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

        public BatManager()
        {
            dictionary = new Dictionary<BatData, BatController>();
            addList = new List<BatData>();
            removeList = new List<BatData>();
            factory = new BatFactory();
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
    }
}
