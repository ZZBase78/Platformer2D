using Platformer2D.Assets.Settings;
using System;
using UnityEngine;

namespace Platformer2D.Assets.Chest
{
    internal sealed class ChestFactory
    {
        private GameObject prefab;

        public ChestFactory()
        {
            prefab = Resources.Load<GameObject>(ResourcesPathes.CHEST_PREFAB);
        }

        public ChestData Create()
        {
            ChestData data = new ChestData();

            GameObject gameObject = GameObject.Instantiate(prefab);
            ChestView view = gameObject.GetComponent<ChestView>();
            if (view == null) throw new Exception(ErrorMessages.CHEST_VIEW_NOT_FOUND);

            data.view = view;

            return data;
        }

        public void Destroy(ChestData data)
        {
            GameObject.Destroy(data.view.gameObject);
            data = null;
        }
    }
}
