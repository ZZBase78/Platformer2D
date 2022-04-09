using Platformer2D.Assets.Settings;
using UnityEngine.SceneManagement;

namespace Platformer2D.Assets.EndLevel
{
    internal sealed class GameNextLevel
    {
        public void Go()
        {
            SceneManager.LoadScene(ScenesNames.MAIN_SCENE);
        }
    }
}
