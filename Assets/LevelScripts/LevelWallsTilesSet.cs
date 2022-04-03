using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer2D.Assets.LevelScripts
{
    [CreateAssetMenu(fileName = "LevelWallsTilesSet", menuName = "ScriptableObjects/Level/LevelWallsTilesSet", order = 1)]
    internal sealed class LevelWallsTilesSet : ScriptableObject
    {
        public Tile wallTile;
        public Tile wallTileUp;
        public Tile wallTileDown;
        public Tile wallTileUpDown;
        public Tile wallTileLeft;
        public Tile wallTileLeftUp;
        public Tile wallTileLeftDown;
        public Tile wallTileLeftUpDown;
        public Tile wallTileRight;
        public Tile wallTileRightUp;
        public Tile wallTileRightDown;
        public Tile wallTileRightUpDown;
        public Tile wallTileRightLeft;
        public Tile wallTileRightLeftUp;
        public Tile wallTileRightLeftDown;
        public Tile wallTileRightLeftUpDown;
    }
}
