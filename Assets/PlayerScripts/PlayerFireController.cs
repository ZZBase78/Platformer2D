using System;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerFireController
    {
        private Player player;
        private Action<Vector2, Vector2> fireAction;
        public PlayerFireController(Player player, Action<Vector2, Vector2> fireAction)
        {
            this.player = player;
            this.fireAction = fireAction;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 position = player.view.transformView.position;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = mousePosition - position;
                fireAction.Invoke(position, direction);
            }
        }
    }
}
