using Platformer2D.Assets.LevelScripts;
using UnityEngine;

namespace Platformer2D.Assets.PlayerScripts
{
    internal sealed class PlayerStartPosition
    {
        public void MoveToStart(Transform transform, LevelData levelData)
        {
            Vector3 position = new Vector3(levelData.levelConfig.levelConfigSettings.wallWidth + 0.5f, levelData.levelConfig.levelConfigSettings.wallHeight + 0.5f, 0f);
            transform.position = position;
        }
    }
}
