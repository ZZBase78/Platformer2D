using Platformer2D.Assets.Settings;
using System;
using UnityEngine;

namespace Platformer2D.Assets.EndLevel
{
    internal sealed class EndLevelUIController
    {
        public event Action actionButtonPressed;

        private const string BUTTON_TEXT_RESTART = "  Restart level  ";
        private const string BUTTON_TEXT_NEXT_LEVEL = "  Next level  ";

        private GameObject prefab;

        private GameObject gameObject;

        public EndLevelUIController()
        {
            prefab = Resources.Load<GameObject>(ResourcesPathes.END_LEVEL_UI_PREFAB);
        }

        private void Show(string buttonText)
        {
            gameObject = GameObject.Instantiate(prefab);

            EndLevelUIView endLevelUIView = gameObject.GetComponent<EndLevelUIView>();
            if (endLevelUIView == null) throw new Exception(ErrorMessages.END_LEVEL_UI_VIEW_NOT_FOUND);

            endLevelUIView.buttonText.text = buttonText;
            endLevelUIView.button.onClick.AddListener(ButtonPressed);
        }

        public void ShowRestart()
        {
            Show(BUTTON_TEXT_RESTART);
        }

        public void ShowNextLevel()
        {
            Show(BUTTON_TEXT_NEXT_LEVEL);
        }

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
            gameObject = null;
        }

        public void ButtonPressed()
        {
            actionButtonPressed?.Invoke();
        }
    }
}
