﻿using UnityEngine.Tilemaps;

namespace Platformer2D.Assets.LevelScripts
{
    internal sealed class LevelElement
    {
        public bool isWall;
        public bool isTiled;
        public Tile tile;

        public LevelElement()
        {
            isWall = false;
            isTiled = false;
            tile = null;
        }
    }
}
