using Platformer2D.Assets.Settings;
using UnityEngine.SceneManagement;

namespace Platformer2D.Assets.EndLevel
{
    internal sealed class GameRestartLevel
    {
        public void Go()
        {
            SceneManager.LoadScene(ScenesNames.MAIN_SCENE);
        }
    }
}
