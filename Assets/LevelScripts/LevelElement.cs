using Platformer2D.Assets.Enemy.Cannon;
using UnityEngine.Tilemaps;

namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelElement
    {
        public bool isWall;
        public bool isCoin;
        public CannonLevelValue cannon;

        public bool isTiled;
        public Tile tile;

        public LevelElement()
        {
            isWall = false;
            isTiled = false;
            isCoin = false;
            tile = null;
            cannon = CannonLevelValue.None;
        }
    }
}
