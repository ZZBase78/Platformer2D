using UnityEngine;

namespace Platformer2D.Assets.Starter
{
    public class Starter : MonoBehaviour
    {
        private Game game;
        void Start()
        {
            game = new Game();
            game.Start();
        }

        void Update()
        {
            game.Update(Time.deltaTime);
        }
    }
}
