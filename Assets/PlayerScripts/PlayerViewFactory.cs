using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerViewFactory
    {
        private GameObject prefab;

        public PlayerViewFactory(GameObject prefab)
        {
            this.prefab = prefab;
        }
        public PlayerView GetPlayerView()
        {
            GameObject gameObject = GameObject.Instantiate(prefab);
            PlayerView playerView = gameObject.GetComponent<PlayerView>();
            if (playerView == null)
            {
                playerView = gameObject.AddComponent<PlayerView>();
            }
            playerView.transformView = gameObject.transform;
            playerView.spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
            playerView.rigidbodyView = gameObject.GetComponentInChildren<Rigidbody2D>();

            return playerView;
        }
    }
}
