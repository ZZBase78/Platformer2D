using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer2D.Assets.LevelScripts.Background
{
    [CreateAssetMenu(fileName = "BackgroundTileSet", menuName = "ScriptableObjects/Level/BackgroundTileSet", order = 1)]
    internal sealed class BackgroundTileSet : ScriptableObject
    {
        public Tile Wall;
    }
}
