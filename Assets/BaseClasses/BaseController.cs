using System.Collections.Generic;

namespace Platformer2D.Assets.BaseClasses
{
    internal abstract class BaseController<T>
    {
        protected List<T> list;
        protected List<T> addList;
        protected List<T> removeList;

        public BaseController()
        {
            list = new List<T>();
            addList = new List<T>();
            removeList = new List<T>();
        }
        public void Update(float deltaTime)
        {
            AddElements();
            RemoveElements();
            foreach (T value in list)
            {
                UpdateElement(value, deltaTime);
            }
        }

        private void AddElements()
        {
            foreach (T value in addList)
            {
                list.Add(value);
            }
            addList.Clear();
        }

        private void RemoveElements()
        {
            foreach (T value in removeList)
            {
                list.Remove(value);
                Destroy(value);
            }
            removeList.Clear();
        }

        protected void AddElement(T value)
        {
            addList.Add(value);
        }

        protected void RemoveElement(T value)
        {
            removeList.Add(value);
        }

        protected abstract void Destroy(T value);
        protected abstract void UpdateElement(T value, float deltaTime);
    }
}
